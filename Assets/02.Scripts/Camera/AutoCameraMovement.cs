using UnityEngine;
using UnityEngine.UI;

public class AutoCameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 카메라 이동 속도
    private bool isBossFight = false; // 보스 전투 중인지 여부를 나타내는 플래그


    public Slider bossHealthSlider;

    void Update()
    {
        if (!isBossFight)
        {
            // 카메라 이동 로직 (보스가 나오기 전까지만 이동)
            MoveCamera();
        }
        else
        {
            ActivateBossHPbar();
        }

        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            moveSpeed = 70f;            //카메라 이동 속도 가속 빌드 시 수정 필요
        }
        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            moveSpeed = 5f;             //카메라 이동 속도 복구 빌드 시 수정 필요
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

    void ActivateBossHPbar()
    {
        // 여기에서 필요한 UI를 활성화하는 코드를 작성
        if (bossHealthSlider != null)
        {
            bossHealthSlider.gameObject.SetActive(true);
        }
    }




    // 보스 전투가 끝났을 때 호출되어 카메라 이동을 다시 시작하는 함수
    //public void ResumeCameraMovement()
    //{
    //    isBossFight = false;
    //}
}
