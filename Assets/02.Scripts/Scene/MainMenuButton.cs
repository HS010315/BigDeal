using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void GoToMainMenu()
    {
        // 0�� ������ �̵�
        SceneManager.LoadScene(0);
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
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }
}