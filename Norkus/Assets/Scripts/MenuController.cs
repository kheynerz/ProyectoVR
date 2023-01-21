using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    [SerializeField] private int sceneBuildIndex;
    [SerializeField] private AudioSource soundtrack;

    public void ChangeScene(){
        SceneManager.LoadScene(sceneBuildIndex);
    }
    public void VolumeUp()
    {
        soundtrack.volume += (float)0.1;
    }
    public void VolumeDown()
    {
        soundtrack.volume -= (float)0.1;
    }

    public void ExitApp()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    //For the gaze to not crash
    public void OnPointerEnter(){
        Debug.Log("EnterPointer");
    }
    public void OnPointerExit(){
        Debug.Log("ExitPointer");
    }
}
