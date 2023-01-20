using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public GameObject _objetoGrabed;
    public GameObject _jugador;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Grab")
        {

            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.transform.SetParent(_jugador.transform,false);
            other.gameObject.transform.localPosition = new Vector3(0f, 1f, 0f);
            _objetoGrabed = other.gameObject;
        }

    
        if (other.gameObject.tag == "UnGrab" && _objetoGrabed != null)
        {

            _objetoGrabed.transform.SetParent(null);
            _objetoGrabed.gameObject.GetComponent<Rigidbody>().useGravity = true;
            _objetoGrabed.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            _objetoGrabed.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            _objetoGrabed.gameObject.transform.position = new Vector3(other.transform.position.x, 1.0f, other.transform.position.z);
            _objetoGrabed.gameObject.transform.rotation = Quaternion.identity;
        }
    }
}
