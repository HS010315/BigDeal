using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public Slider gaugeSlider; // Unity 에디터에서 연결할 Slider UI 요소
    private bool isShiftKeyPressed = false;

    private void Start()
    {
        gaugeSlider.minValue = 0;
        gaugeSlider.maxValue = 100;
        gaugeSlider.value = 100; // 초기 게이지 값 설정
    }

    void Update()
    {
        // 왼쪽 쉬프트 키가 눌렸는지 확인
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 왼쪽 쉬프트 키가 눌려 있으면 게이지를 초당 10씩 감소시킴
            isShiftKeyPressed = true;
        }
        else
        {
            isShiftKeyPressed = false;
        }

        // 왼쪽 쉬프트 키가 눌렸을 때 게이지를 감소시키고, 떼면 게이지를 증가시킴
        if (isShiftKeyPressed)
        {
            gaugeSlider.value -= 10 * Time.deltaTime; // 초당 10씩 감소시킴 (Time.deltaTime을 곱해 초당 감소량으로 변환)
        }
        else
        {
            gaugeSlider.value += 10 * Time.deltaTime; // 왼쪽 쉬프트 키가 떼어져 있을 때 초당 10씩 증가시킴
        }

        // 게이지가 범위를 벗어나지 않도록 보정
        gaugeSlider.value = Mathf.Clamp(gaugeSlider.value, gaugeSlider.minValue, gaugeSlider.maxValue);

        // 게이지가 0 이하로 떨어지면 게임 오버 또는 필요한 동작 수행
        if (gaugeSlider.value <= 0)
        {
            Debug.Log("게이지가 0 이하로 떨어졌습니다. 게임 오버 또는 필요한 동작 수행");
            // 필요한 동작 수행 코드를 여기에 추가하세요.
        }
    }
}