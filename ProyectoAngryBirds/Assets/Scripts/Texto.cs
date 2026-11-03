using UnityEngine;
using TMPro;

public class Texto : MonoBehaviour
{

    public TextMeshProUGUI textoSushi;
    public TextMeshProUGUI textoEnemigos;
    public static Texto instancia;
    public GameObject imagenVictoria;

    public int SushisRestantes = 10;
    public int EnemigosEliminados = 0;
    private int enemigosRestantes;

    void Start()
    {
        enemigosRestantes = GameObject.FindGameObjectsWithTag("Enemigos").Length;  
        imagenVictoria.SetActive(false);                                          
        ActualizarTextos();
        
    }


    void Awake()
    {
        instancia = this;
    }

    public bool HaySushis()
    {
        return SushisRestantes > 0;
    }

    public void RestarSushi()
    {
        if (SushisRestantes > 0)
        {
            SushisRestantes--;
            ActualizarTextos();
        }
    }

    public void SumarPuntos(int puntos)
    {
        EnemigosEliminados += puntos;
        enemigosRestantes--;
        ActualizarTextos();

        if (enemigosRestantes <= 0)           
        {
            imagenVictoria.SetActive(true);
        }
    }

    void Update()
    {

    }

    void ActualizarTextos()
    {
        textoSushi.text = "Limite de tiros: " + SushisRestantes;
        textoEnemigos.text = "Puntaje: " + EnemigosEliminados;
    }
}
