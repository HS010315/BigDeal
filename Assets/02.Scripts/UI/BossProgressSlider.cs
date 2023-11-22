using UnityEngine;
using UnityEngine.UI;

public class BossProgressSlider : MonoBehaviour
{
    public Slider progressSlider; // Unity 에디터에서 슬라이더를 연결합니다.
    public Transform player; // 플레이어의 Transform을 연결합니다.
    public Transform boss; // 보스가 등장하는 X 좌표

    private void Update()
    {
        // 플레이어의 X 좌표가 보스 등장 지점을 넘어가면 진행 상황을 업데이트합니다.
        if (player.position.x <= boss.position.x)
        {
            // 진행 상황을 계산하고 슬라이더를 업데이트합니다.
            UpdateProgressSlider();
        }
        if(player.position.x >= boss.position.x)
        {
            progressSlider.gameObject.SetActive(false);
        }
    }

    public void UpdateProgressSlider()
    {
        // 보스 등장 전의 전체 거리
        float totalDistance = boss.position.x;

        // 플레이어가 이동한 거리
        float playerDistance = boss.position.x - player.position.x;


        progressSlider.value = playerDistance;
        progressSlider.maxValue = totalDistance;
    }
}