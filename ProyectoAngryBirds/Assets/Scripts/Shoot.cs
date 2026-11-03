using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public GameObject sushiPrefab;
    public Transform puntoLanzamiento;
    public float fuerza = 8f;

    private Controles controles;
    private GameObject sushi;
    private Rigidbody2D rb;
    bool arrastrando;

    void Start()
    {
        CrearSushi();
    }

    void Awake()
    {
        controles = new Controles();
    }

    void OnEnable()
    {
       controles.Posiciones.Enable();
    }

    void OnDisable()
    {
        controles.Posiciones.Disable();
    }


    void Update()
    {
        if (sushi == null) return;

        if (controles.Posiciones.Arrastrar.WasPressedThisFrame())
        {
            arrastrando = true;
        }

            if (arrastrando)
        {
            Vector2 jalon = PosicionMouse() - (Vector2)puntoLanzamiento.position;
            jalon = Vector2.ClampMagnitude(jalon, 2f);
            sushi.transform.position = (Vector2)puntoLanzamiento.position + jalon;


            if (controles.Posiciones.Arrastrar.WasReleasedThisFrame())
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddForce(-jalon * fuerza, ForceMode2D.Impulse);

                arrastrando = false;
                Destroy(sushi, 8f);
                sushi = null;

                Texto.instancia.RestarSushi();

                if (Texto.instancia.HaySushis())
                {
                    Invoke("CrearSushi", 1.5f);
                }
            }
        }
    }

    void CrearSushi()
    {
        sushi = Instantiate(sushiPrefab, puntoLanzamiento.position, Quaternion.identity);
        rb = sushi.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    Vector2 PosicionMouse()
    {
        Vector2 pantalla = controles.Posiciones.posicion.ReadValue<Vector2>();
        Vector3 mundo = Camera.main.ScreenToWorldPoint(pantalla);
        return new Vector2(mundo.x, mundo.y);
    }
}
