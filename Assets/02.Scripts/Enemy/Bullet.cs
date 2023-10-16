using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // ���� ī�޶� �����մϴ�.
    }

    void Update()
    {
        // �Ѿ��� ���� ��ġ�� ȭ�� ��ǥ�� ��ȯ
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);

        // ���� �Ѿ��� ī�޶� �þ� ������ ������ ����
        if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
        {
            Destroy(gameObject);
        }
    }
}