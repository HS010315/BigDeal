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
    private bool isInvincible = false; // 무적 상태 변수 추가


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

            // 무적 상태인 경우 충돌 처리를 스킵
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
            // 플레이어의 입력을 기반으로 대쉬 방향 설정
            Vector2 dashInput = new Vector2(moveX, moveY);
            if (dashInput.magnitude > 0.1f) // 입력이 존재할 때만 대쉬
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
        //게이지 전부 소모 시 추락
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
            rb.velocity = dashDirection * dashSpeed;    //점프도 인식이 되서 대쉬 방향이 이상함.
            dashTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector3.zero;
        isDashing = false;
        co.enabled = true;

        yield return new WaitForSeconds(DashCoolTime);  //왜 쿨타임 적용이 안되지 ?
        //대쉬 중 무적 판정

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
        else if (collision.gameObject.CompareTag("Enemy") && !isInvincible) // 무적 상태가 아닐 때만 처리
        {
            PlayerDied();
        }
    }

    private void GameOver()
    {
        //UI 팝업 후 Restart 버튼으로 씬 처음부터 다시 불러옴. + 추가 요소 필요
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

        // 리스폰 후 무적 상태 활성화
        isInvincible = true;

        // 2초 후에 무적 상태 비활성화
        StartCoroutine(DisableInvincibility());
    }

    private IEnumerator DisableInvincibility()
    {
        yield return new WaitForSeconds(3f); // 3초 대기

        // 무적 상태 비활성화
        isInvincible = false;
    }


}