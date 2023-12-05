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
        // "Esc" Ű�� ������ �� ���� �Ͻ� ����/�簳 ���
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
        // isPaused �� ����
        isPaused = !isPaused;

        // ���� �Ͻ� ���� �Ǵ� �簳
        if (isPaused)
        {
            Time.timeScale = 0f; // ���� �Ͻ� ����
        }
        else
        {
            Time.timeScale = 1f; // ���� �簳
        }

        Debug.Log("Game paused: " + isPaused);
    }


}