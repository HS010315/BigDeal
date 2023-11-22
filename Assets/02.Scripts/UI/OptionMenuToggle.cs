using UnityEngine;

public class OptionMenuToggle : MonoBehaviour
{
    public Canvas optionCanvas;

    void Start()
    {
        // 초기에는 옵션 캔버스를 비활성화
        if (optionCanvas != null)
        {
            optionCanvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // "Esc" 키를 눌렀을 때 옵션 캔버스의 활성화/비활성화를 토글
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionMenu();
        }
    }

    void ToggleOptionMenu()
    {
        if (optionCanvas != null)
        {
            // 현재 상태의 반대로 설정
            optionCanvas.gameObject.SetActive(!optionCanvas.gameObject.activeSelf);
        }
    }
}