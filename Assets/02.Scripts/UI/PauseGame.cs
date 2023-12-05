using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject restartButton;
    public GameObject OptionCanvas;

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

    public void GoToMainMenu()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
        }
        SceneManager.LoadScene(0);
        OptionCanvas.gameObject.SetActive(false);
        isPaused = false;
        DestroyAllEnemies();
        DestroyAllBullet();
        DestroyAllBoss();
    }
    private void DestroyAllEnemies()
    {
        // ��� enemy �±׸� ���� ������Ʈ�� ã�Ƽ� ����
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    private void DestroyAllBoss()
    {
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        foreach (GameObject boss in bosses)
        {
            Destroy(boss);
        }
    }

    private void DestroyAllBullet()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }


}