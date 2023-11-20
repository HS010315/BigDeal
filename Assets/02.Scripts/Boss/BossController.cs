using UnityEngine;

public class BossController : MonoBehaviour
{
    public AutoCameraMovement cameraMovementScript;
    public GameObject shotObject; // Shot 오브젝트 프리팹 설정
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
            // 보스가 카메라 안에 있을 때 Shot 오브젝트와 shotMulti 오브젝트 활성화
            if (shotObject != null && !shotObject.activeSelf)
            {
                shotObject.SetActive(true);
            }
            if (shotMultiObject != null && !shotMultiObject.activeSelf)
            {
                shotMultiObject.SetActive(true);
            }
            if (Time.time % 10 == 0)  // 10초에 한 번씩 실행
            {
                DropWoodColumn();
            }

            // 카메라 이동을 멈춤
            cameraMovementScript.StopCameraMovement();
        }
        else
        {
            // 보스가 카메라 밖에 있을 때 Shot 오브젝트와 shotMulti 오브젝트 비활성화
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

    // TakeDamage 메서드 추가
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
        // 카메라의 현재 위치와 크기 가져오기
        Camera mainCamera = Camera.main;
        Vector3 cameraPosition = mainCamera.transform.position;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // 보스의 위치 확인
        Vector3 bossPosition = transform.position;

        // 보스가 카메라 안에 있는지 확인
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
        // 보스가 카메라 안에 있을 때만 실행
        if (IsBossInsideCamera())
        {
            // WarningLine 컴포넌트를 가져옴
            WarningLine warningLine = GetComponent<WarningLine>();

            // dropPosition.position을 전달하여 ShowWarningBeforeDrop 메서드 호출
            warningLine.ShowWarningBeforeDrop(dropPosition.position);

            // 나머지 코드는 그대로 유지
            GameObject woodColumn = Instantiate(woodColumnPrefab, dropPosition.position, Quaternion.identity);

            // Rigidbody 컴포넌트를 추가하여 중력의 영향을 받도록 함
            Rigidbody rb = woodColumn.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = woodColumn.AddComponent<Rigidbody>();
            }

            // 아래로 떨어지도록 설정
            rb.useGravity = true;
            rb.isKinematic = false; // 중력 영향을 받도록 함

            rb.velocity = Vector3.down * fallingSpeed;
        }
    }
}