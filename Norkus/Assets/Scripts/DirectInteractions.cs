using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectInteractions : MonoBehaviour
{
    Dictionary<string, string> prefixes =  new Dictionary<string, string>();
    // Start is called before the first frame update

    private string[] soundNames = new string[]{"death1","death2","death3"};

    void Start()
    {
        //Posible direct Interactions
        prefixes.Add("ZB", "HitPlayer");
    }


    void Awake(){
        StartCoroutine(DeathSound());
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
       
        GameObject go = new GameObject();
        AudioSource audioSource = go.AddComponent<AudioSource>();

        int rand = Random.Range(0,3);



        audioSource.clip = Resources.Load<AudioClip>("DeathSounds/"+soundNames[rand]);
        audioSource.Play();
        go.tag = "Destroy";
        DontDestroyOnLoad(go);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartCoroutine(DeathSound());
    }

    IEnumerator DeathSound(){
        yield return new WaitForSeconds(3);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Destroy")){
            Destroy(go);
        } 
    }    
}
