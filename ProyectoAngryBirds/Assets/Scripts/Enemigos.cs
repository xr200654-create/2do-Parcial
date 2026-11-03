using UnityEngine;
using TMPro;

public class Enemigos : MonoBehaviour
{
    public int golpesMaximos = 3;
    public int puntosPorMuerte = 1;

    private int golpesActuales = 0;
    private bool muerto = false;

    void Start()
    {
       
    }

    void Desactivar()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D infocollision2D)
    {

        if (muerto) return;

        if (infocollision2D.gameObject.CompareTag("Proyectil"))
        {
            golpesActuales++;

            GetComponent<SpriteRenderer>().color = Color.red;

            if (golpesActuales >= golpesMaximos)
            {
                gameObject.tag = "Untagged";
               
                Destroy(gameObject);
            }

            
        }

        if (infocollision2D.gameObject.CompareTag("Madera"))
        {
            golpesActuales++;

            if (golpesActuales >= golpesMaximos)
            {
                gameObject.tag = "Untagged";

                Destroy(gameObject);
            }

           
        }

        if (golpesActuales >= golpesMaximos)
        {
            Morir();
        }
    }

    void Morir()
    {
        muerto = true;
        gameObject.tag = "Untagged";
        Texto.instancia.SumarPuntos(puntosPorMuerte);
        Destroy(gameObject);
    } 


    void Update()
    {
        
    }
}
