using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletSpawnPoint; // 총알 발사 위치를 지정하기 위한 Transform 컴포넌트
    public GameObject bulletPrefab;    // 총알 프리팹

    public float bulletSpeed = 20f;    // 총알 속도
    public float fireRate = 0.5f;      // 발사 간격 (초당 발사 횟수)

    private float nextFireTime = 0f;   // 다음 발사 가능한 시간

    public int damage = 10; 

    void Update()
    {
        // K 키를 누르고 발사 간격을 지난 경우에만 총알을 발사합니다.
        if (Input.GetKeyDown(KeyCode.K) && Time.time > nextFireTime)
        {
            Shoot(); // 총알 발사 함수 호출
            nextFireTime = Time.time + 1 / fireRate; // 다음 발사 가능한 시간 설정
        }
    }

    void Shoot()
    {
        // 총알을 생성하고 발사 위치로 이동시킵니다.
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // 총알에 Rigidbody를 추가하고 속도를 설정하여 발사합니다.
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawnPoint.right * bulletSpeed;

        // 총알이 화면 밖으로 나가면 일정 시간 후에 파괴됩니다.
        Destroy(bullet, 3f);

        
    }


}
