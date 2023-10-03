using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject explosionEffect; // 폭발 효과 프리팹 설정
    private int hitsRemaining = 3; // 플레이어의 남은 목숨 횟수

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) // 탄막에 부딪혔을 때
        {
            Destroy(other.gameObject); // 탄막 파괴

            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity); // 폭발 효과 생성
            }

            // 플레이어에 데미지 입히기
            hitsRemaining--;

            // 남은 목숨이 0이하이면 플레이어를 파괴하고 콘솔에 메시지 출력
            if (hitsRemaining <= 0)
            {
                PlayerDied();
            }
        }
    }

    void PlayerDied()
    {

        if (gameObject != null)
        {
            Destroy(gameObject); // 플레이어 오브젝트 파괴
        }
        Debug.Log("Player Died"); 
    }
}