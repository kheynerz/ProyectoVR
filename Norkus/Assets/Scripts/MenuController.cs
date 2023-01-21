using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    [SerializeField] private int sceneBuildIndex;

    public void ChangeScene(){
        SceneManager.LoadScene(sceneBuildIndex);
    }
    
    public void ExitApp()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
