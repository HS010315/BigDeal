using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas startCanvas;
    public Canvas optionCanvas;
    // Start is called before the first frame update
    void Start()
    {
        startCanvas.gameObject.SetActive(true);
        optionCanvas.gameObject.SetActive(false);
    }

    public void OnclickOptionButton()
    {
        startCanvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(true);
    }

    public void OnclickBackButton()
    {
        startCanvas.gameObject.SetActive(true);
        optionCanvas.gameObject.SetActive(false);
    }
}
