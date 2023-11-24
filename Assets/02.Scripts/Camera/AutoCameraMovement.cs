using UnityEngine;
using UnityEngine.UI;

public class AutoCameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ī�޶� �̵� �ӵ�
    private bool isBossFight = false; // ���� ���� ������ ���θ� ��Ÿ���� �÷���


    public Slider bossHealthSlider;

    void Update()
    {
        if (!isBossFight)
        {
            // ī�޶� �̵� ���� (������ ������ �������� �̵�)
            MoveCamera();
        }
        else
        {
            ActivateBossHPbar();
        }
    }



    void MoveCamera()
    {
        // ī�޶��� ���� ��ġ�� ������
        Vector3 currentPosition = transform.position;

        // ī�޶� ���������� �̵���Ŵ (x ������ �̵�)
        currentPosition.x += moveSpeed * Time.deltaTime;

        // ���ο� ��ġ�� ī�޶� ����
        transform.position = currentPosition;

        
    }

    // ������ ��Ÿ�� �� ȣ��Ǿ� ī�޶� �̵��� ���ߴ� �Լ�
    public void StopCameraMovement()
    {
        isBossFight = true;
    }

    void ActivateBossHPbar()
    {
        // ���⿡�� �ʿ��� UI�� Ȱ��ȭ�ϴ� �ڵ带 �ۼ�
        if (bossHealthSlider != null)
        {
            bossHealthSlider.gameObject.SetActive(true);
        }
    }




    // ���� ������ ������ �� ȣ��Ǿ� ī�޶� �̵��� �ٽ� �����ϴ� �Լ�
    //public void ResumeCameraMovement()
    //{
    //    isBossFight = false;
    //}
}
