using UnityEngine;
using System.Collections.Generic;

public class CameraPointerManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _maxDistance = 15;
    private GameObject _gazedAtObject = null;

    [SerializeField] private GameObject pointer;
  
    private readonly string interactableTag = "Interactable";
    Dictionary<string, string> prefixes =  new Dictionary<string, string>();

    private void Start()
    {
        GazeManager.Instance.OnGazeSelection += GazeSelection;

        //Interactables in Menu
        prefixes.Add("CS", "ChangeScene");
        prefixes.Add("EX", "ExitApp");


        prefixes.Add("ZB", "KillZombie");
        
    }

    private void GazeSelection()
    {
        if (!_gazedAtObject.CompareTag(interactableTag) && !_gazedAtObject.CompareTag("InteractableMenu")) return;
       
        string value = "";
        if (prefixes.TryGetValue(_gazedAtObject.name.Substring(0,2), out value)){
            _gazedAtObject?.SendMessage(value, null, SendMessageOptions.DontRequireReceiver);
        }
    }


    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {   
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
               
                if (_gazedAtObject?.tag == "Interactable"){
                    _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);

                }
                _gazedAtObject = hit.transform.gameObject;
                if (_gazedAtObject?.tag == "Interactable"){
                    _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
                }
                if (hit.transform.CompareTag(interactableTag) || hit.transform.CompareTag("InteractableMenu"))
                    GazeManager.Instance.StartGazeSelection();
            }
        }
        else
        {
            GazeManager.Instance.CancelGazeSelection();
            if (_gazedAtObject?.tag == "Interactable"){
                _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
            }
            _gazedAtObject = null;
        }
    }
}
