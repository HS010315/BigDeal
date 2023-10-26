using UnityEngine;

public class BossController : MonoBehaviour
{
    public AutoCameraMovement cameraMovementScript;
    public GameObject shotObject; // Shot 오브젝트 프리팹 설정
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
            // 보스가 카메라 안에 있을 때 Shot 오브젝트와 shotMulti 오브젝트 활성화
            if (shotObject != null && !shotObject.activeSelf)
            {
                shotObject.SetActive(true);
            }
            if (shotMultiObject != null && !shotMultiObject.activeSelf)
            {
                shotMultiObject.SetActive(true);
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
        // 카메라의 현재 위치와 크기 가져오기
        Camera mainCamera = Camera.main;
        Vector3 cameraPosition = mainCamera.transform.position;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // 보스의 위치 확인
        Vector3 bossPosition = transform.position;

        // 보스가 카메라 안에 있는지 확인
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