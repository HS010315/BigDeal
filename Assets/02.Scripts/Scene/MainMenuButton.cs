using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void GoToMainMenu()
    {
        // 0번 씬으로 이동
        SceneManager.LoadScene(0);
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
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }
}