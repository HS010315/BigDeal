using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float flySpeed = 7f;
    public float dashSpeed = 20f;
    public float DashTime = 0.1f;
    private bool isDashing = false;
    private Vector3 dashDirection;
    public float jumpForce = 2f;
    private bool canJump = false;
    private bool isFlying = false;
    private bool isDie = false;
    public GameObject explosionEffect;
    public Transform respawnPosition;
    private bool isInvincible = false; // ���� ���� ���� �߰�
    public GaugeController gaugeController;
    public int playerLife = 3;
    public List<Image> heartImages;
    private float lastDashTime; // ������ �뽬 �ð� ���
    public float dashCooldown = 2f; // �뽬 ��Ÿ�� (��: 2��)
    public GameObject gameOverPanel;

    public ParticleSystem waterbomb;

    public Transform bulletSpawnPoint; // �Ѿ� �߻� ��ġ�� �����ϱ� ���� Transform ������Ʈ
    public GameObject bulletPrefab;    // �Ѿ� ������
    public float bulletSpeed = 20f;    // �Ѿ� �ӵ�
    public float fireRate = 0.5f;      // �߻� ���� (�ʴ� �߻� Ƚ��)
    private float nextFireTime = 0f;   // ���� �߻� ������ �ð�

    public int damage = 10;


    private Rigidbody rb;
    public Collider co;
    public Animator ani;

    public float invincibilityDuration = 3f; // ���� ���� �ð�
    public float blinkInterval = 0.2f;        // �����̴� ����
    public Renderer characterRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();

        GameObject mainCamera = Camera.main.gameObject;
        Physics.IgnoreCollision(mainCamera.GetComponent<Collider>(), GetComponent<Collider>());

        UpdateLifeUI();
        lastDashTime = -dashCooldown;
        characterRenderer = GetComponent<Renderer>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);

            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            // ���� ������ ��� �浹 ó���� ��ŵ
            if (isInvincible)
            {
                return;
            }

            PlayerDied();
        }
    }
    
    void UpdateLifeUI()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            if (i < playerLife)
            {
                heartImages[i].enabled = true; // ��� ����ŭ �̹��� Ȱ��ȭ
            }
            else
            {
                heartImages[i].enabled = false; // ���� �̹��� ��Ȱ��ȭ
            }
        }
    }

    public void PlayerDied()
    {
        playerLife--;

        if (playerLife <= 0)
        {
            playerLife = 0;
            UpdateLifeUI(); // UI ������Ʈ
            GameOver(); // ���ӿ��� ó��
        }
        else
        {
            UpdateLifeUI(); // UI ������Ʈ
            RespawnPlayer(); // �÷��̾� ������ ó��
        }
    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            playerLife = 100;                       //ü�� 100 ���� ��ǥ �� ���� �� ����
        }
                
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (moveX != 0 || moveY != 0 && !isFlying)
        {
            ani.SetBool("Run", true);
        }

        else
        {
            ani.SetBool("Run", false);
        }

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            ani.SetTrigger("Jump");
            rb.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
            canJump = false;
        }
       if (Input.GetKeyDown(KeyCode.L) && !isDashing && Time.time - lastDashTime > dashCooldown)
        {
            // �뽬 ������ ��쿡�� ����
            Vector2 dashInput = new Vector2(moveX, moveY);
            if (dashInput.magnitude > 0.1f)
            {
                dashDirection = dashInput.normalized;
                co.enabled = false;
                StartCoroutine(Dash());

                // �뽬 ���� �� ������ �뽬 �ð� ���
                lastDashTime = Time.time;
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ani.SetBool("Fly", true);
            waterbomb.Play();
            isFlying = true;
        }
        else
        {
            ani.SetBool("Fly", false);
            waterbomb.Stop();
        }
        if (isFlying)
        {
            Fly();
        }

        if (!isFlying && Input.GetKey(KeyCode.K) && Time.time > nextFireTime)
        {
            ani.SetBool("Run", true);
            Shoot();
            nextFireTime = Time.time + 1 / fireRate;
        }

        if(isFlying && Input.GetKey(KeyCode.K) && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1 / fireRate;
        }

    }

    public void Fly()
    {
        if (gaugeController.gaugeSlider.value > 3) // ������ �� Ȯ��
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            rb.velocity = new Vector3(moveX * flySpeed, moveY * flySpeed, rb.velocity.z);

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isFlying = false;
            }

        }
        else
        {
            // �������� ������� �� �÷��̾�� ������ �� �����ϴ�.
            isFlying = false;
            waterbomb.Stop();
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        float dashTime = 0f;
        rb.velocity = Vector3.zero;
        while (dashTime < DashTime)
        {
            rb.velocity = dashDirection * dashSpeed;    //������ �ν��� �Ǽ� �뽬 ������ �̻���.
            dashTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector3.zero;
        isDashing = false;
        co.enabled = true;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
        else if (collision.gameObject.CompareTag("Enemy") && !isInvincible) // ���� ���°� �ƴ� ���� ó��
        {
            PlayerDied();
        }
    }

    private void GameOver()
    {
        //UI �˾� �� Restart ��ư���� �� ó������ �ٽ� �ҷ���. + �߰� ��� �ʿ�
        Destroy(gameObject);
        Debug.Log("die");
        gameOverPanel.SetActive(true);
    }

    private void RespawnPlayer()
    {
        if (isDie)
        {
            gameObject.SetActive(true);
            isDie = false;
        }
        transform.position = new Vector3(respawnPosition.position.x, respawnPosition.position.y, 0); // Z �� ���� 0���� ����
        Debug.Log("respawn");
        isInvincible = true;
        StartCoroutine(DisableInvincibility());
        StartCoroutine(BlinkEffect());
        UpdateLifeUI();
    }

    private IEnumerator DisableInvincibility()
    {
        yield return new WaitForSeconds(3f); // 3�� ���

        // ���� ���� ��Ȱ��ȭ
        isInvincible = false;
    }

    private IEnumerator BlinkEffect()
    {
        while (isInvincible)
        {
            // ĳ���͸� ���̰ų� �����
            characterRenderer.enabled = !characterRenderer.enabled;

            // ���� ���� ���� ��ٸ���
            yield return new WaitForSeconds(blinkInterval);
        }

        // ���� ���°� �����Ǹ� ĳ���͸� �ٽ� ���̰� ����
        characterRenderer.enabled = true;
    }
}