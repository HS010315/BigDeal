using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    void Start()
    {
        // �ʱ� ������ UI�� ǥ��
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        // ���� ���� �� ȣ��Ǵ� �޼���
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        // UI Text�� ���� ������ ������Ʈ
        scoreText.text = "Score: " + score;
    }
}
