using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjectRay : MonoBehaviour
{

    public GameObject _attachPoint;
    private float _scaleX;
    private float _scaleY;
    private float _scaleZ;


    private void Start()
    {
        _scaleX = this.transform.localScale.x;
        _scaleY = this.transform.localScale.y;
        _scaleZ = this.transform.localScale.z;
    }


    public void OnGrabRayInteractionAttach()
    {
          this.gameObject.GetComponent<Rigidbody>().useGravity = false;
          this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
          this.gameObject.transform.SetParent(_attachPoint.transform, false);
          this.gameObject.transform.localPosition = new Vector3(0f, 1f, 0f);
          
    }

    public void OnGrabRayInteractionDeAttach()
    {
        // Solo para un hijo
        Transform _detachObject = _attachPoint.transform.GetChild(0);
        _detachObject.transform.SetParent(null);
        _detachObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
        _detachObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        _detachObject.gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        _detachObject.gameObject.transform.position = new Vector3(this.transform.position.x, 1f, this.transform.position.z);
        _detachObject.gameObject.transform.rotation = Quaternion.identity;
    }
}
