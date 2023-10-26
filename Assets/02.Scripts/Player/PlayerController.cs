using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float DashTime = 0.1f;
    private bool isDashing = false;
    private Vector3 dashDirection;
    public float jumpForce = 2f;
    private bool canJump = true;
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

    private Rigidbody rb;
    public Collider co;
    private Animator ani;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();


        GameObject mainCamera = Camera.main.gameObject;
        Physics.IgnoreCollision(mainCamera.GetComponent<Collider>(), GetComponent<Collider>());

        UpdateLifeUI();
        lastDashTime = -dashCooldown;
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
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
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
        if (Input.GetKey(KeyCode.LeftShift) && !isFlying)
        {
            isFlying = true;
        }
        if (isFlying)
        {
            Fly();
        }
    }

    public void Fly()
    {
        if (gaugeController.gaugeSlider.value > 0) // ������ �� Ȯ��
        {
            float moveY = Input.GetAxis("Vertical");
            rb.velocity = new Vector3(rb.velocity.x, moveY * moveSpeed, rb.velocity.z);

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isFlying = false;
            }
        }
        else
        {
            // �������� ������� �� �÷��̾�� ������ �� �����ϴ�.
            isFlying = false;
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
        transform.position = respawnPosition.position;
        Debug.Log("respawn");

        // ������ �� ���� ���� Ȱ��ȭ
        isInvincible = true;

        // 2�� �Ŀ� ���� ���� ��Ȱ��ȭ
        StartCoroutine(DisableInvincibility());

        UpdateLifeUI();
    }

    private IEnumerator DisableInvincibility()
    {
        yield return new WaitForSeconds(3f); // 3�� ���

        // ���� ���� ��Ȱ��ȭ
        isInvincible = false;
    }
}