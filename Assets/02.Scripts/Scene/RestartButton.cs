using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void RestartGame()
    {
        // ���� ����� �ڵ� (��: 0�� �� �ٽ� �ε�)
        SceneManager.LoadScene(1);

        // ���� ���� �г� ��Ȱ��ȭ
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        DestroyAllEnemies();
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
}