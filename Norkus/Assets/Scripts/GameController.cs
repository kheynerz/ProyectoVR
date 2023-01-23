using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private int killsRequired;
    private int kills;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckKills()
    {
        this.kills += 1;
        if (this.kills == killsRequired)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
