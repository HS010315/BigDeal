using UnityEngine;

public class EnemyController : MonoBehaviour
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

    void Start()
    {
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
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();

            if (bulletRB != null)
            {
                Vector3 shootDirection = (player.position - bulletSpawnPoint.position).normalized;
                bulletRB.velocity = shootDirection * bulletSpeed;
            }
        }
    }
}