using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();

            // �߷� ������ ���� �ʵ��� ����
            playerRigidbody.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();

            // �߷� ������ �ٽ� �޵��� ����
            playerRigidbody.useGravity = true;
        }
    }
}