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
    public float speed = 5f;
    public float rotationSpeed = 200.0f;
    public float jumpForce = 5f;
    private Animator animator;
    public float x , y;
    private float moveInput = 0f;
    private float turnInput = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        bocina = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        textoPuntos = GameObject.Find("txtPuntos").GetComponent<TextMeshProUGUI>();
        textoPuntos.text = "Puntos: " + puntos;
        speed = velocidad;

       
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.angularDrag = 5f;
        rb.maxAngularVelocity = 2f;
    }

 
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        turnInput = x;
        moveInput = y;

        animator.SetFloat("SpeedX", x);
        animator.SetFloat("SpeedY", y);
    }

    void FixedUpdate()
    {
        Quaternion turn = Quaternion.Euler(0f, turnInput * rotationSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turn);

        Vector3 movement = transform.forward * moveInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        Vector3 ang = rb.angularVelocity;
        ang.x = 0f;
        ang.z = 0f;
        rb.angularVelocity = ang;

        float yAngle = rb.rotation.eulerAngles.y;
        rb.MoveRotation(Quaternion.Euler(0f, yAngle, 0f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.CompareTag("Puntos"))
        {
            puntos += obj.GetComponent<Puntos>().puntos;
            bocina.PlayOneShot(sonidoPuntos);

            textoPuntos.text = "Puntos: " + puntos;
            Destroy(obj);
            if (puntos >= 30)
            {
                SceneManager.LoadScene(1);
            }
        }
        else if (obj.CompareTag("RestPuntos"))
        {
            puntos -= obj.GetComponent<Puntos>().puntos;
            bocina.PlayOneShot(sonidoNegativo);
            textoPuntos.text = "Puntos: " + puntos;
            Destroy(obj);


        }


    }
}
