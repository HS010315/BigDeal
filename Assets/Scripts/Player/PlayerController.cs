using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float DashTime = 0.1f;
    public float DashCoolTime = 5f;
    private bool isDashing = false;
    private Vector3 dashDirection;
    public float jumpForce = 2f;
    private bool canJump = true;
    private bool isFlying = false;
    private bool isDie = false;
    public int playerLife = 3;
    public GameObject explosionEffect;
    public Transform respawnPosition;
    private bool isInvincible = false; // ���� ���� ���� �߰�


    private Rigidbody rb;
    public Collider co;
    private Animator ani;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();


        GameObject mainCamera = Camera.main.gameObject;
        Physics.IgnoreCollision(mainCamera.GetComponent<Collider>(), GetComponent<Collider>());

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
    public void PlayerDied()
    {
        playerLife--;

        gameObject.SetActive(false);
        isDie = true;

        if (playerLife <= 0)
        {
            GameOver();
        }
        else
        {
            RespawnPlayer();
        }
        return;
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
        if (Input.GetKeyDown(KeyCode.L) && !isDashing)
        {
            // �÷��̾��� �Է��� ������� �뽬 ���� ����
            Vector2 dashInput = new Vector2(moveX, moveY);
            if (dashInput.magnitude > 0.1f) // �Է��� ������ ���� �뽬
            {
                dashDirection = dashInput.normalized;
                co.enabled = false;
                StartCoroutine(Dash());
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

    private void Fly()
    {
        float moveY = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(rb.velocity.x, moveY * moveSpeed, rb.velocity.z);
        //������ ���� �Ҹ� �� �߶�
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
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

        yield return new WaitForSeconds(DashCoolTime);  //�� ��Ÿ�� ������ �ȵ��� ?
        //�뽬 �� ���� ����

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
    }

    private IEnumerator DisableInvincibility()
    {
        yield return new WaitForSeconds(3f); // 3�� ���

        // ���� ���� ��Ȱ��ȭ
        isInvincible = false;
    }


}