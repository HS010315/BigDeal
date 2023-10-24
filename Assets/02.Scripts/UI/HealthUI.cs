using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts; // ��Ʈ �̹��� �迭

    public void SetHealth(int health)
    {
        // ü�¿� ���� ��Ʈ �̹����� ǥ�� �Ǵ� ����ϴ�.
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                // ���� ü�º��� ���� �ε����� ��Ʈ �̹����� ǥ��
                hearts[i].enabled = true;
            }
            else
            {
                // ���� ü�� �̻��� �ε����� ��Ʈ �̹����� ����
                hearts[i].enabled = false;
            }
        }
    }
}