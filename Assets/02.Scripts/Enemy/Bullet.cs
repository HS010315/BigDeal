using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // 메인 카메라를 참조합니다.
    }

    void Update()
    {
        // 메인 카메라가 존재하는지 확인
        if (mainCamera != null)
        {
            // 총알의 현재 위치를 화면 좌표로 변환
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);

            // 만약 총알이 카메라 시야 밖으로 나가면 삭제
            if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // 메인 카메라가 없는 경우 예외 처리 또는 적절한 대책을 수행
            Debug.LogWarning("Main Camera is missing!");
        }
    }
}