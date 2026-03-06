using UnityEngine;
using TMPro;

public class Interactuar : MonoBehaviour
{
    public Dialogo dialogo;
    public GameObject textoInteractuar;

    bool jugadorCerca = false;

    void Start()
    {
        textoInteractuar.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogo.EstaActivo())
            {
                textoInteractuar.SetActive(false);
                dialogo.IniciarDialogo();
            }
            else
            {
                textoInteractuar.SetActive(false);
                dialogo.IniciarDialogo();  // ? abre el diálogo
            }
        }
        if (dialogo.EstaActivo() && Input.anyKeyDown)
        {
            dialogo.SiguienteLinea();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Jugador entro");

        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            textoInteractuar.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            textoInteractuar.SetActive(false);
            dialogo.CerrarDialogo();
        }
    }


}