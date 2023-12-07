using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Canvas startCanvas;
    public Canvas optionCanvas;
    public Canvas htpCanvas;

    void Start()
    {
        startCanvas.gameObject.SetActive(true);       
    }


    public void OnclickOptionButton()
    {
        optionCanvas.gameObject.SetActive(true);
    }

    public void OnclickBackButton()
    {
        optionCanvas.gameObject.SetActive(false);
        htpCanvas.gameObject.SetActive(false);

    }

    public void OnclickHTPButton()
    {
        htpCanvas.gameObject.SetActive(true);

    }

    public void OnclickNLButton()
    {
        SceneManager.LoadScene(2);
    }

}
