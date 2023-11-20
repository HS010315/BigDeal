using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();

            // 중력 영향을 받지 않도록 설정
            playerRigidbody.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();

            // 중력 영향을 다시 받도록 설정
            playerRigidbody.useGravity = true;
        }
    }
}