using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void NextLevelButton()
    {
        // ���� ����� �ڵ� (��: 0�� �� �ٽ� �ε�)
        SceneManager.LoadScene(2);

        // ���� ���� �г� ��Ȱ��ȭ
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        DestroyAllEnemies();
        DestroyAllBullet();
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

    private void DestroyAllBullet()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }
}