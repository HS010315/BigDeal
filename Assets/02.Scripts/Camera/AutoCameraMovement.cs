using UnityEngine;

public class AutoCameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 카메라 이동 속도
    private bool isBossFight = false; // 보스 전투 중인지 여부를 나타내는 플래그

    void Update()
    {
        if (!isBossFight)
        {
            // 카메라 이동 로직 (보스가 나오기 전까지만 이동)
            MoveCamera();
        }
    }

    void MoveCamera()
    {
        // 카메라의 현재 위치를 가져옴
        Vector3 currentPosition = transform.position;

        // 카메라를 오른쪽으로 이동시킴 (x 축으로 이동)
        currentPosition.x += moveSpeed * Time.deltaTime;

        // 새로운 위치를 카메라에 적용
        transform.position = currentPosition;
    }

    // 보스가 나타날 때 호출되어 카메라 이동을 멈추는 함수
    public void StopCameraMovement()
    {
        isBossFight = true;
    }

    // 보스 전투가 끝났을 때 호출되어 카메라 이동을 다시 시작하는 함수
    //public void ResumeCameraMovement()
    //{
    //    isBossFight = false;
    //}
}
