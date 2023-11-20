using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject opitonCanvas;
    // Start is called before the first frame update
    void Start()
    {
        startCanvas.SetActive(true);
        opitonCanvas.SetActive(false);
    }

    // Update is called once per frame

}
