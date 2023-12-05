using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void NextLevelButton()
    {
        // 게임 재시작 코드 (예: 0번 씬 다시 로드)
        SceneManager.LoadScene(2);

        // 게임 오버 패널 비활성화
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        DestroyAllEnemies();
        DestroyAllBullet();
    }
    private void DestroyAllEnemies()
    {
        // 모든 enemy 태그를 가진 오브젝트를 찾아서 삭제
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    private void DestroyAllBullet()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }
}