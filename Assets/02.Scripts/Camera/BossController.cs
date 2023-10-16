using UnityEngine;

public class BossController : MonoBehaviour
{
    public AutoCameraMovement cameraMovementScript;
    public GameObject shotObject; // Shot ������Ʈ ������ ����

    void Start()
    {
        cameraMovementScript = FindObjectOfType<AutoCameraMovement>();
    }

    void Update()
    {
        if (IsBossInsideCamera())
        {
            // ������ ī�޶� �ȿ� ���� �� Shot ������Ʈ Ȱ��ȭ
            if (shotObject != null && !shotObject.activeSelf)
            {
                shotObject.SetActive(true);
            }

            // ī�޶� �̵��� ����
            cameraMovementScript.StopCameraMovement();
        }
        else
        {
            // ������ ī�޶� �ۿ� ���� �� Shot ������Ʈ ��Ȱ��ȭ
            if (shotObject != null && shotObject.activeSelf)
            {
                shotObject.SetActive(false);
            }
        }
    }

    bool IsBossInsideCamera()
    {
        // ī�޶��� ���� ��ġ�� ũ�� ��������
        Camera mainCamera = Camera.main;
        Vector3 cameraPosition = mainCamera.transform.position;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // ������ ��ġ Ȯ��
        Vector3 bossPosition = transform.position;

        // ������ ī�޶� �ȿ� �ִ��� Ȯ��
        if (bossPosition.x >= cameraPosition.x - cameraWidth / 2f && bossPosition.x <= cameraPosition.x + cameraWidth / 2f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void BossDefeated()
    {
        // ���� ������ ������ �� ī�޶� �̵��� �ٽ� ����
        cameraMovementScript.ResumeCameraMovement();
    }
}