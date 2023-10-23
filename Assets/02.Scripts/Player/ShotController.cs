using UnityEngine;

public class ShotController : MonoBehaviour
{
    public int damage = 10; // �Ѿ� ������

    void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �����ɴϴ�.
        GameObject hitObject = other.gameObject;

        // �浹�� ������Ʈ�� �� ������Ʈ���� Ȯ���մϴ�.
        if (hitObject.CompareTag("Enemy"))
        {
            // �� ������Ʈ�� EnemyController ��ũ��Ʈ�� ã�Ƽ� �������� �����ϴ�.
            EnemyController enemy = hitObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // �Ѿ� ������Ʈ�� �����մϴ�.
            Destroy(gameObject);
        }
        if (hitObject.CompareTag("Boss"))
        {
            // �� ������Ʈ�� EnemyController ��ũ��Ʈ�� ã�Ƽ� �������� �����ϴ�.
            BossController Boss = hitObject.GetComponent<BossController>();
            if (Boss != null)
            {
                Boss.TakeDamage(damage);
            }

            // �Ѿ� ������Ʈ�� �����մϴ�.
            Destroy(gameObject);
        }
    }
}