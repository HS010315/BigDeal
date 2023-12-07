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
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            startCanvas.gameObject.SetActive(true);
        }

        Button exitButton = GetComponent<Button>();

        // 만약 버튼이 존재하면
        if (exitButton != null)
        {
            // 클릭 이벤트에 메서드 연결
            exitButton.onClick.AddListener(ExitGameOnClick);
        }
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

    public void ExitGameOnClick()
    {
        // 게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
