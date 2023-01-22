using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectInteractions : MonoBehaviour
{
    Dictionary<string, string> prefixes =  new Dictionary<string, string>();
    // Start is called before the first frame update
    void Start()
    {
        //Posible direct Interactions
        prefixes.Add("ZB", "HitPlayer");
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Dead") return;
        if (other.name.Substring(0,2) == "PU"){
            gameObject?.SendMessage("PowerUp",null, SendMessageOptions.DontRequireReceiver);
            other?.SendMessage("DestroyPowerUp",null, SendMessageOptions.DontRequireReceiver);
            return;
        }

        string value = "";
        if (prefixes.TryGetValue(other.name.Substring(0,2), out value)){
            other?.SendMessage(value, null, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void KillPlayer(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
