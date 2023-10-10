using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject explosionEffect; // ���� ȿ�� ������ ����
    private int hitsRemaining = 3; // �÷��̾��� ���� ��� Ƚ��

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) // ź���� �ε����� ��
        {
            Destroy(other.gameObject); // ź�� �ı�

            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity); // ���� ȿ�� ����
            }

            // �÷��̾ ������ ������
            hitsRemaining--;

            // ���� ����� 0�����̸� �÷��̾ �ı��ϰ� �ֿܼ� �޽��� ���
            if (hitsRemaining <= 0)
            {
                PlayerDied();
            }
        }
    }

    void PlayerDied()
    {

        if (gameObject != null)
        {
            Destroy(gameObject); // �÷��̾� ������Ʈ �ı�
        }
        Debug.Log("Player Died"); 
    }
}