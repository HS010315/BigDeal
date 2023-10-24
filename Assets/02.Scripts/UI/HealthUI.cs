using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts; // 하트 이미지 배열

    public void SetHealth(int health)
    {
        // 체력에 따라 하트 이미지를 표시 또는 숨깁니다.
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                // 현재 체력보다 작은 인덱스의 하트 이미지를 표시
                hearts[i].enabled = true;
            }
            else
            {
                // 현재 체력 이상의 인덱스의 하트 이미지를 숨김
                hearts[i].enabled = false;
            }
        }
    }
}