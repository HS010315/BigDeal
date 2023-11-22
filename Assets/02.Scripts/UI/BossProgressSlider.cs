using UnityEngine;
using UnityEngine.UI;

public class BossProgressSlider : MonoBehaviour
{
    public Slider progressSlider; // Unity �����Ϳ��� �����̴��� �����մϴ�.
    public Transform player; // �÷��̾��� Transform�� �����մϴ�.
    public Transform boss; // ������ �����ϴ� X ��ǥ

    private void Update()
    {
        // �÷��̾��� X ��ǥ�� ���� ���� ������ �Ѿ�� ���� ��Ȳ�� ������Ʈ�մϴ�.
        if (player.position.x <= boss.position.x)
        {
            // ���� ��Ȳ�� ����ϰ� �����̴��� ������Ʈ�մϴ�.
            UpdateProgressSlider();
        }
        if(player.position.x >= boss.position.x)
        {
            progressSlider.gameObject.SetActive(false);
        }
    }

    public void UpdateProgressSlider()
    {
        // ���� ���� ���� ��ü �Ÿ�
        float totalDistance = boss.position.x;

        // �÷��̾ �̵��� �Ÿ�
        float playerDistance = boss.position.x - player.position.x;


        progressSlider.value = playerDistance;
        progressSlider.maxValue = totalDistance;
    }
}