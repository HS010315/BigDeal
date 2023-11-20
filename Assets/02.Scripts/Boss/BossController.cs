using UnityEngine;

public class BossController : MonoBehaviour
{
    public AutoCameraMovement cameraMovementScript;
    public GameObject shotObject; // Shot ������Ʈ ������ ����
    public GameObject shotMultiObject;
    public float MaxHealth = 100.0f;
    public float CurrentHealth;
    public GameObject gameClearPanel;
    public GameObject woodColumnPrefab;
    public Transform dropPosition;
    public float fallingSpeed = 5f;

    void Start()
    {
        cameraMovementScript = FindObjectOfType<AutoCameraMovement>();
        CurrentHealth = MaxHealth;
        InvokeRepeating("DropWoodColumn", 0f, 10f);
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
            if (Time.time % 10 == 0)  // 10�ʿ� �� ���� ����
            {
                DropWoodColumn();
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

    // TakeDamage �޼��� �߰�
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
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
        if (bossPosition.x >= cameraPosition.x - cameraWidth / 2f && bossPosition.x + 4f <= cameraPosition.x + cameraWidth / 2f)
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
        if (gameClearPanel != null)
        {
            gameClearPanel.SetActive(true);
        }
    }

    void DropWoodColumn()
    {
        // ������ ī�޶� �ȿ� ���� ���� ����
        if (IsBossInsideCamera())
        {
            // WarningLine ������Ʈ�� ������
            WarningLine warningLine = GetComponent<WarningLine>();

            // dropPosition.position�� �����Ͽ� ShowWarningBeforeDrop �޼��� ȣ��
            warningLine.ShowWarningBeforeDrop(dropPosition.position);

            // ������ �ڵ�� �״�� ����
            GameObject woodColumn = Instantiate(woodColumnPrefab, dropPosition.position, Quaternion.identity);

            // Rigidbody ������Ʈ�� �߰��Ͽ� �߷��� ������ �޵��� ��
            Rigidbody rb = woodColumn.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = woodColumn.AddComponent<Rigidbody>();
            }

            // �Ʒ��� ���������� ����
            rb.useGravity = true;
            rb.isKinematic = false; // �߷� ������ �޵��� ��

            rb.velocity = Vector3.down * fallingSpeed;
        }
    }
}