using UnityEngine;

public class ShotController : MonoBehaviour
{
    public int damage = 10; // 총알 데미지

    void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트를 가져옵니다.
        GameObject hitObject = other.gameObject;

        // 충돌한 오브젝트가 적 오브젝트인지 확인합니다.
        if (hitObject.CompareTag("Enemy"))
        {
            // 적 오브젝트의 EnemyController 스크립트를 찾아서 데미지를 입힙니다.
            EnemyController enemy = hitObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            else
            {
                // EnemyController가 없는 경우 EnemyController1을 찾아서 데미지를 입힙니다.
                EnemyController1 enemy1 = hitObject.GetComponent<EnemyController1>();
                if (enemy1 != null)
                {
                    enemy1.TakeDamage(damage);
                }
            }

            // 총알 오브젝트를 제거합니다.
            Destroy(gameObject);
        }
        if (hitObject.CompareTag("Boss"))
        {
            // 적 오브젝트의 EnemyController 스크립트를 찾아서 데미지를 입힙니다.
            BossController Boss = hitObject.GetComponent<BossController>();
            if (Boss != null)
            {
                Boss.TakeDamage(damage);
            }
            else
            {
                BossController2 Boss1 = hitObject.GetComponent<BossController2>();
                if (Boss1 != null)
                {
                    Boss1.TakeDamage(damage);
                }
            }

            // 총알 오브젝트를 제거합니다.
            Destroy(gameObject);
        }
    }
}