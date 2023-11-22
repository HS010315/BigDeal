using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas startCanvas;
    public Canvas optionCanvas;


    public void OnclickOptionButton()
    {
        optionCanvas.gameObject.SetActive(true);
    }

    public void OnclickBackButton()
    {
        optionCanvas.gameObject.SetActive(false);
    }
}
