using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moverJugador : MonoBehaviour
{
    public float velocidad = 5f;
    TextMeshProUGUI textoPuntos;
    int puntos = 0;

    AudioSource bocina;
    public AudioClip sonidoPuntos;
    public AudioClip sonidoNegativo;
    Rigidbody rb;

    void Start()
    {
        bocina = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        textoPuntos = GameObject.Find("txtPuntos").GetComponent<TextMeshProUGUI>();
        textoPuntos.text = "Puntos: " + puntos;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(horizontal, 0f, vertical) * velocidad * Time.deltaTime;
        rb.MovePosition(rb.position + movimiento);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;// Obtener el objeto con el que colisionamos (puede ser un punto o un restapunto)

        if (obj.CompareTag("Puntos")) // Si el objeto tiene la etiqueta "Puntos" 
        {
            puntos += obj.GetComponent<Puntos>().puntos; // Sumar los puntos del objeto al total de puntos del jugador
            bocina.PlayOneShot(sonidoPuntos);

            textoPuntos.text = "Puntos: " + puntos;// Actualizar el texto de puntos en la interfaz de usuario
            Destroy(obj);
            if (puntos >= 30)// Si los puntos del jugador son menores o iguales a 0, cargar la escena de Game Over (escena con índice 1)
            {
                SceneManager.LoadScene(1);
            }
        }
        else if (obj.CompareTag("RestPuntos"))// Si el objeto tiene la etiqueta "RestPuntos"
        {
            puntos -= obj.GetComponent<RestPuntos>().puntos;// Restar los puntos del objeto al total de puntos del jugador 
            bocina.PlayOneShot(sonidoNegativo);
            textoPuntos.text = "Puntos: " + puntos;// Actualizar el texto de puntos en la interfaz de usuario
            Destroy(obj);

           
        }
       

    }

}
