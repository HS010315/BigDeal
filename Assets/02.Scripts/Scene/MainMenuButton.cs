using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void GoToMainMenu()
    {
        // 0�� ������ �̵�
        SceneManager.LoadScene(0);
    }
}