using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        // "Esc" Ű�� ������ �� ���� �Ͻ� ����/�簳 ���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
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