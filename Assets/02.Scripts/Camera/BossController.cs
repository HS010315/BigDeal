using UnityEngine;

public class BossController : MonoBehaviour
{
    public AutoCameraMovement cameraMovementScript;
    public GameObject shotObject; // Shot ������Ʈ ������ ����
    public GameObject shotMultiObject;
    public float MaxHealth = 100.0f;
    public float CurrentHealth;

    void Start()
    {
        cameraMovementScript = FindObjectOfType<AutoCameraMovement>();
        CurrentHealth = MaxHealth;
    }

   void Update()
    {
        if (IsBossInsideCamera())
        {
            // ������ ī�޶� �ȿ� ���� �� Shot ������Ʈ�� shotMulti ������Ʈ Ȱ��ȭ
            if (shotObject != null && !shotObject.activeSelf)
            {
                shotObject.SetActive(true);
            }
            if (shotMultiObject != null && !shotMultiObject.activeSelf)
            {
                shotMultiObject.SetActive(true);
            }

            // ī�޶� �̵��� ����
            cameraMovementScript.StopCameraMovement();
        }
        else
        {
            // ������ ī�޶� �ۿ� ���� �� Shot ������Ʈ�� shotMulti ������Ʈ ��Ȱ��ȭ
            if (shotObject != null && shotObject.activeSelf)
            {
                shotObject.SetActive(false);
            }
            if (shotMultiObject != null && shotMultiObject.activeSelf)
            {
                shotMultiObject.SetActive(false);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if(CurrentHealth <= 0)
        {
            BossDefeated();
        }
    }
    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }
    public float GetMaxHealth()
    {
        return MaxHealth;
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
        if (bossPosition.x  >= cameraPosition.x - cameraWidth / 2f && bossPosition.x + 4f <= cameraPosition.x + cameraWidth / 2f)
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
        Destroy(gameObject);
        //cameraMovementScript.ResumeCameraMovement();
    }
}