using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void GoToMainMenu()
    {
        // 0번 씬으로 이동
        SceneManager.LoadScene(0);
    }
}