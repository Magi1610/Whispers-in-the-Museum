using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logica_player : MonoBehaviour
{
    public float velocidad = 5f;
    TextMeshProUGUI textoPuntos;
    int puntos = 0;
    AudioSource bocina;
    public AudioClip sonidoPuntos;
    public AudioClip sonidoNegativo;
    Rigidbody rb;
    public float speed = 5f; // Velocidad de movimiento del jugador
    public float rotationSpeed = 200.0f; // Velocidad de rotación del jugador
    public float jumpForce = 5f; // Fuerza de salto del jugador
    private Animator animator; // Referencia al componente Animator del jugador
    public float x , y; // Variables para almacenar la entrada del jugador
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Obtener el componente Animator del jugador
        bocina = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        textoPuntos = GameObject.Find("txtPuntos").GetComponent<TextMeshProUGUI>();
        textoPuntos.text = "Puntos: " + puntos;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal"); // Obtener la entrada horizontal del jugador
        y = Input.GetAxis("Vertical"); // Obtener la entrada vertical del jugador

        transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0); // Rotar el jugador en el eje Y según la entrada horizontal
        transform.Translate(0, 0, y * speed * Time.deltaTime); // Mover el jugador hacia adelante o hacia atrás según la entrada vertical

        animator.SetFloat("SpeedX", x);
        animator.SetFloat("SpeedY", y);

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
