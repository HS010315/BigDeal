using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public GameObject StartCanvas;
    public GameObject LevelCanvas;
    private bool isPaused = false;
    void Start()
    {
        StartCanvas.SetActive(true);
        LevelCanvas.SetActive(false);
    }

    public void Startbutton()
    {
        StartCanvas.SetActive(false);
        LevelCanvas.SetActive(true);
    }

    public void GardenSceneButton()
    {
        SceneManager.LoadScene(1);
    }


    public void DesertSceneButton()
    {
        SceneManager.LoadScene(2);
    }

    public void MenuButton()
    {
        StartCanvas.SetActive(true);
        LevelCanvas.SetActive(false);
    }


}