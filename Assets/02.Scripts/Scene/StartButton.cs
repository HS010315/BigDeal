using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private bool isPaused = false;
    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
        isPaused = false;
       
    }


}