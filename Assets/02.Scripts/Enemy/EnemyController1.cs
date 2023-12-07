using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    private Transform player;
    private Rigidbody rb;
    private bool shouldMove = true;
    public float moveSpeed = 5f;
    public float shootInterval = 2f;
    private float timeSinceLastShot = 0f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public int health = 100;
    public int scoreValue = 100;
    public GameObject deathEffectPrefab;

    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (shouldMove && player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.Normalize();
            rb.velocity = directionToPlayer * moveSpeed;
        }

        // 총 발사 로직
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= shootInterval)
        {
            Shoot();
            timeSinceLastShot = 0f;
        }
        if (transform.position.x + 15f < Camera.main.transform.position.x)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MainCamera"))
        {
            shouldMove = false;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
            // 플레이어를 향해 총을 쏩니다.
            Shoot();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null && player != null)
        {
            Vector3 shootDirection = (player.position - bulletSpawnPoint.position).normalized;

            // 첫 번째 방향
            FireBullet(shootDirection);

            // 30도씩 차이 나는 방향으로 11번 반복하여 발사
            for (int i = 0; i < 2; i++)
            {
                float angle = -90f * (i + 1);
                Quaternion rotation = Quaternion.Euler(0, 0, angle);

                FireBullet(rotation * shootDirection);
            }
        }
    }

    void FireBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();

        if (bulletRB != null)
        {
            bulletRB.velocity = direction * bulletSpeed;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        // 체력이 0 이하로 떨어졌을 때 적 오브젝트를 제거
        if (health <= 0)
        {
            Die();
        }
    }

    // 적 오브젝트를 제거하는 함수
    void Die()
    {
        if (deathEffectPrefab != null)
        {
            // 파티클 효과 생성
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        scoreManager.AddScore(scoreValue);
        Destroy(gameObject);
    }
}