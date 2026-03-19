using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject obj_pos;

    private GameObject pickedObject = null;
    void Update()
    {
       if (Input.GetKey("r"))
        {
            pickedObject.GetComponent<Rigidbody>().useGravity = true;
            pickedObject.GetComponent<Rigidbody>().isKinematic = false;
            pickedObject.gameObject.transform.SetParent(null);
            pickedObject = null;

        }



    }


    private void OnTriggerStay(Collider other)
    {
       if (other.gameObject.CompareTag("Pickup"))
        {
            if (Input.GetKey("e") && pickedObject == null)
            {
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic= true;
                other.transform.position = obj_pos.transform.position;
                other.gameObject.transform.SetParent(obj_pos.gameObject.transform);
                pickedObject = other.gameObject;


            }
        }
        // Mover objeto a la mano suavemente (sin SetParent)
        if (pickedObject != null && obj_pos != null) // ← agregar && obj_pos != null
        {
            pickedObject.transform.position = Vector3.Lerp(
                pickedObject.transform.position,
                obj_pos.transform.position,
                Time.deltaTime * 15f
            );
            pickedObject.transform.rotation = obj_pos.transform.rotation;
        }
    }


}