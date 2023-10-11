using UnityEngine;

public class BossController : MonoBehaviour
{
    public AutoCameraMovement cameraMovementScript;
    public GameObject shotObject; // Shot ������Ʈ ������ ����
    public float detectionDistance = 10f; // ī�޶� ���� �Ÿ� ����

    void Start()
    {
        cameraMovementScript = FindObjectOfType<AutoCameraMovement>();
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < detectionDistance)
            {
                // �÷��̾ ���� ��ó�� ���� �� Shot ������Ʈ Ȱ��ȭ
                if (shotObject != null && !shotObject.activeSelf)
                {
                    shotObject.SetActive(true);
                }

                // ī�޶� �̵��� ����
                cameraMovementScript.StopCameraMovement();
            }
            else
            {
                // �÷��̾ �־����� �� Shot ������Ʈ ��Ȱ��ȭ
                if (shotObject != null && shotObject.activeSelf)
                {
                    shotObject.SetActive(false);
                }

                // ī�޶� �̵��� �簳
                //cameraMovementScript.ResumeCameraMovement();
            }
        }
    }

    void BossDefeated()
    {
        // ���� ������ ������ �� ī�޶� �̵��� �ٽ� ����
        cameraMovementScript.ResumeCameraMovement();
    }
}
