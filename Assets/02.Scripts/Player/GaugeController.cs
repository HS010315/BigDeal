using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public Slider gaugeSlider; // Unity �����Ϳ��� ������ Slider UI ���
    private bool isShiftKeyPressed = false;

    private void Start()
    {
        gaugeSlider.minValue = 0;
        gaugeSlider.maxValue = 100;
        gaugeSlider.value = 100; // �ʱ� ������ �� ����
    }

    void Update()
    {
        // ���� ����Ʈ Ű�� ���ȴ��� Ȯ��
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // ���� ����Ʈ Ű�� ���� ������ �������� �ʴ� 10�� ���ҽ�Ŵ
            isShiftKeyPressed = true;
        }
        else
        {
            isShiftKeyPressed = false;
        }

        // ���� ����Ʈ Ű�� ������ �� �������� ���ҽ�Ű��, ���� �������� ������Ŵ
        if (isShiftKeyPressed)
        {
            gaugeSlider.value -= 20 * Time.deltaTime; // �ʴ� 10�� ���ҽ�Ŵ (Time.deltaTime�� ���� �ʴ� ���ҷ����� ��ȯ)
        }
        else
        {
            gaugeSlider.value += 5 * Time.deltaTime; // ���� ����Ʈ Ű�� ������ ���� �� �ʴ� 10�� ������Ŵ
        }

        // �������� ������ ����� �ʵ��� ����
        gaugeSlider.value = Mathf.Clamp(gaugeSlider.value, gaugeSlider.minValue, gaugeSlider.maxValue);

        
       
    }
}