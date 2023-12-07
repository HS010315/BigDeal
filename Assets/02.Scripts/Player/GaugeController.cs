using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public Slider gaugeSlider; // Connect Slider UI element in the Unity Editor
    private bool isShiftKeyPressed = false;

    // �� ������������ ����� ������ ��뷮�� �ν����� â���� ����
    public float usageRate = 20.0f;

    private void Start()
    {
        gaugeSlider.minValue = 0;
        gaugeSlider.maxValue = 100;
        gaugeSlider.value = 100; // Set the initial gauge value
    }

    void Update()
    {
        // ���� ����Ʈ Ű�� ���ȴ��� Ȯ��
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // ���� ����Ʈ Ű�� ���� ������ �������� ���ҽ�Ŵ
            isShiftKeyPressed = true;
        }
        else
        {
            isShiftKeyPressed = false;
        }

        // ������ usageRate�� ���� ������ ��뷮�� ����
        if (isShiftKeyPressed)
        {
            gaugeSlider.value -= usageRate * Time.deltaTime;
        }
        else
        {
            gaugeSlider.value += 3 * Time.deltaTime;
        }

        // �������� ������ ����� �ʵ��� ����
        gaugeSlider.value = Mathf.Clamp(gaugeSlider.value, gaugeSlider.minValue, gaugeSlider.maxValue);
    }
}
