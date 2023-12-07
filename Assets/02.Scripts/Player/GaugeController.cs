using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public Slider gaugeSlider; // Connect Slider UI element in the Unity Editor
    private bool isShiftKeyPressed = false;

    // 각 스테이지에서 사용할 게이지 사용량을 인스펙터 창에서 설정
    public float usageRate = 20.0f;

    private void Start()
    {
        gaugeSlider.minValue = 0;
        gaugeSlider.maxValue = 100;
        gaugeSlider.value = 100; // Set the initial gauge value
    }

    void Update()
    {
        // 왼쪽 쉬프트 키가 눌렸는지 확인
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 왼쪽 쉬프트 키가 눌려 있으면 게이지를 감소시킴
            isShiftKeyPressed = true;
        }
        else
        {
            isShiftKeyPressed = false;
        }

        // 설정된 usageRate에 따라 게이지 사용량을 적용
        if (isShiftKeyPressed)
        {
            gaugeSlider.value -= usageRate * Time.deltaTime;
        }
        else
        {
            gaugeSlider.value += 3 * Time.deltaTime;
        }

        // 게이지가 범위를 벗어나지 않도록 보정
        gaugeSlider.value = Mathf.Clamp(gaugeSlider.value, gaugeSlider.minValue, gaugeSlider.maxValue);
    }
}
