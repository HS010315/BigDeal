using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    void Start()
    {
        // 초기 점수를 UI에 표시
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        // 적을 죽일 때 호출되는 메서드
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        // UI Text에 현재 점수를 업데이트
        scoreText.text = "Score: " + score;
    }
}
