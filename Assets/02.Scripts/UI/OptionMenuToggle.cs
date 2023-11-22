using UnityEngine;

public class OptionMenuToggle : MonoBehaviour
{
    public Canvas optionCanvas;

    void Start()
    {
        // �ʱ⿡�� �ɼ� ĵ������ ��Ȱ��ȭ
        if (optionCanvas != null)
        {
            optionCanvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // "Esc" Ű�� ������ �� �ɼ� ĵ������ Ȱ��ȭ/��Ȱ��ȭ�� ���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionMenu();
        }
    }

    void ToggleOptionMenu()
    {
        if (optionCanvas != null)
        {
            // ���� ������ �ݴ�� ����
            optionCanvas.gameObject.SetActive(!optionCanvas.gameObject.activeSelf);
        }
    }
}