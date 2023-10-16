using UnityEngine;

public class BossController : MonoBehaviour
{
    public AutoCameraMovement cameraMovementScript;
    public GameObject shotObject; // Shot 오브젝트 프리팹 설정

    void Start()
    {
        cameraMovementScript = FindObjectOfType<AutoCameraMovement>();
    }

    void Update()
    {
        if (IsBossInsideCamera())
        {
            // 보스가 카메라 안에 있을 때 Shot 오브젝트 활성화
            if (shotObject != null && !shotObject.activeSelf)
            {
                shotObject.SetActive(true);
            }

            // 카메라 이동을 멈춤
            cameraMovementScript.StopCameraMovement();
        }
        else
        {
            // 보스가 카메라 밖에 있을 때 Shot 오브젝트 비활성화
            if (shotObject != null && shotObject.activeSelf)
            {
                shotObject.SetActive(false);
            }
        }
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
        // 보스 전투가 끝났을 때 카메라 이동을 다시 시작
        cameraMovementScript.ResumeCameraMovement();
    }
}