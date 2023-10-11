using UnityEngine;

public class BossController : MonoBehaviour
{
    public AutoCameraMovement cameraMovementScript;
    public GameObject shotObject; // Shot 오브젝트 프리팹 설정
    public float detectionDistance = 10f; // 카메라 멈춤 거리 설정

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
                // 플레이어가 보스 근처로 왔을 때 Shot 오브젝트 활성화
                if (shotObject != null && !shotObject.activeSelf)
                {
                    shotObject.SetActive(true);
                }

                // 카메라 이동을 멈춤
                cameraMovementScript.StopCameraMovement();
            }
            else
            {
                // 플레이어가 멀어졌을 때 Shot 오브젝트 비활성화
                if (shotObject != null && shotObject.activeSelf)
                {
                    shotObject.SetActive(false);
                }

                // 카메라 이동을 재개
                //cameraMovementScript.ResumeCameraMovement();
            }
        }
    }

    void BossDefeated()
    {
        // 보스 전투가 끝났을 때 카메라 이동을 다시 시작
        cameraMovementScript.ResumeCameraMovement();
    }
}
