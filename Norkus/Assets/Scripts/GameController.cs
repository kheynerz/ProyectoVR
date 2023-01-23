using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private int killsRequired;
    private GameObject AK;
    private GameObject SN;
    private GameObject BA;
    private int kills;
    public void CheckKills()
    {
        this.kills += 1;
        if (this.kills == killsRequired){
            GameObject[] soundtracks = GameObject.FindGameObjectsWithTag("Music");
            foreach (GameObject soundtrack in soundtracks){
                Destroy(soundtrack);
            } 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    void Awake(){
                
        Transform mainCamera = gameObject.transform.GetChild(0);

        AK = mainCamera.Find("AK").gameObject;
        AK.SetActive(false);

        SN = mainCamera.Find("SN").gameObject;
        SN.SetActive(false);

        BA = mainCamera.Find("BA").gameObject;
        BA.SetActive(false);

        int selectedGun = PlayerPrefs.GetInt("player",1);

        if (selectedGun == 1){
            AK.SetActive(true);
            return;
        }

        if (selectedGun == 2){
            SN.SetActive(true);
            return;
        }
        
        BA.SetActive(true);
    }


}
