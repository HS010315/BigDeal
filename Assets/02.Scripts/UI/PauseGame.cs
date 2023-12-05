using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject restartButton;
    public GameObject OptionCanvas;


    void Start()
    {
        Time.timeScale = 1f;
    }
    void Update()
    {
        // "Esc" 키를 눌렀을 때 게임 일시 정지/재개 토글
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void OnClickRestartButton()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene(1);
        OptionCanvas.gameObject.SetActive(false);
        isPaused = false;
    }

    public void OnClickBackButton()
    {
        Time.timeScale = 1f;
        OptionCanvas.SetActive(false);
        isPaused = false;
    }

    void TogglePause()
    {
        // isPaused 값 반전
        isPaused = !isPaused;

        // 게임 일시 정지 또는 재개
        if (isPaused)
        {
            Time.timeScale = 0f; // 게임 일시 정지
        }
        else
        {
            Time.timeScale = 1f; // 게임 재개
        }

        Debug.Log("Game paused: " + isPaused);
    }


}