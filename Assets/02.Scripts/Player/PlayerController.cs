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
    private bool isInvincible = false; // 무적 상태 변수 추가
    public GaugeController gaugeController;
    public int playerLife = 3;
    public List<Image> heartImages;
    private float lastDashTime; // 마지막 대쉬 시간 기록
    public float dashCooldown = 2f; // 대쉬 쿨타임 (예: 2초)


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

            // 무적 상태인 경우 충돌 처리를 스킵
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
                heartImages[i].enabled = true; // 목숨 수만큼 이미지 활성화
            }
            else
            {
                heartImages[i].enabled = false; // 남은 이미지 비활성화
            }
        }
    }

    public void PlayerDied()
    {
        playerLife--;

        if (playerLife <= 0)
        {
            playerLife = 0;
            UpdateLifeUI(); // UI 업데이트
            GameOver(); // 게임오버 처리
        }
        else
        {
            UpdateLifeUI(); // UI 업데이트
            RespawnPlayer(); // 플레이어 리스폰 처리
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
    // 대쉬 가능한 경우에만 실행
    Vector2 dashInput = new Vector2(moveX, moveY);
    if (dashInput.magnitude > 0.1f)
    {
        dashDirection = dashInput.normalized;
        co.enabled = false;
        StartCoroutine(Dash());

        // 대쉬 실행 후 마지막 대쉬 시간 기록
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
        if (gaugeController.gaugeSlider.value > 0) // 게이지 값 확인
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
            // 게이지가 비어있을 때 플레이어는 비행할 수 없습니다.
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

        UpdateLifeUI();
    }

    private IEnumerator DisableInvincibility()
    {
        yield return new WaitForSeconds(3f); // 3초 대기

        // 무적 상태 비활성화
        isInvincible = false;
    }
}