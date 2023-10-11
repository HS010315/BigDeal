using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletSpawnPoint; // �Ѿ� �߻� ��ġ�� �����ϱ� ���� Transform ������Ʈ
    public GameObject bulletPrefab;    // �Ѿ� ������

    public float bulletSpeed = 20f;    // �Ѿ� �ӵ�
    public float fireRate = 0.5f;      // �߻� ���� (�ʴ� �߻� Ƚ��)

    private float nextFireTime = 0f;   // ���� �߻� ������ �ð�

    public int damage = 10; 

    void Update()
    {
        // K Ű�� ������ �߻� ������ ���� ��쿡�� �Ѿ��� �߻��մϴ�.
        if (Input.GetKeyDown(KeyCode.K) && Time.time > nextFireTime)
        {
            Shoot(); // �Ѿ� �߻� �Լ� ȣ��
            nextFireTime = Time.time + 1 / fireRate; // ���� �߻� ������ �ð� ����
        }
    }

    void Shoot()
    {
        // �Ѿ��� �����ϰ� �߻� ��ġ�� �̵���ŵ�ϴ�.
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // �Ѿ˿� Rigidbody�� �߰��ϰ� �ӵ��� �����Ͽ� �߻��մϴ�.
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawnPoint.right * bulletSpeed;

        // �Ѿ��� ȭ�� ������ ������ ���� �ð� �Ŀ� �ı��˴ϴ�.
        Destroy(bullet, 3f);

        
    }


}
