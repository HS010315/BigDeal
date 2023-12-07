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
    private bool isInvincible = false; // 무적 상태 변수 추가
    public GaugeController gaugeController;
    public int playerLife = 3;
    public List<Image> heartImages;
    private float lastDashTime; // 마지막 대쉬 시간 기록
    public float dashCooldown = 2f; // 대쉬 쿨타임 (예: 2초)
    public GameObject gameOverPanel;

    public ParticleSystem waterbomb;

    public Transform bulletSpawnPoint; // 총알 발사 위치를 지정하기 위한 Transform 컴포넌트
    public GameObject bulletPrefab;    // 총알 프리팹
    public float bulletSpeed = 20f;    // 총알 속도
    public float fireRate = 0.5f;      // 발사 간격 (초당 발사 횟수)
    private float nextFireTime = 0f;   // 다음 발사 가능한 시간

    public int damage = 10;


    private Rigidbody rb;
    public Collider co;
    public Animator ani;

    public float invincibilityDuration = 3f; // 무적 지속 시간
    public float blinkInterval = 0.2f;        // 깜빡이는 간격
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
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            playerLife = 100;                       //체력 100 설정 발표 용 빌드 시 삭제
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
        if (gaugeController.gaugeSlider.value > 3) // 게이지 값 확인
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
            // 게이지가 비어있을 때 플레이어는 비행할 수 없습니다.
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
            rb.velocity = dashDirection * dashSpeed;    //점프도 인식이 되서 대쉬 방향이 이상함.
            dashTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector3.zero;
        isDashing = false;
        co.enabled = true;
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
        gameOverPanel.SetActive(true);
    }

    private void RespawnPlayer()
    {
        if (isDie)
        {
            gameObject.SetActive(true);
            isDie = false;
        }
        transform.position = new Vector3(respawnPosition.position.x, respawnPosition.position.y, 0); // Z 축 값을 0으로 설정
        Debug.Log("respawn");
        isInvincible = true;
        StartCoroutine(DisableInvincibility());
        StartCoroutine(BlinkEffect());
        UpdateLifeUI();
    }

    private IEnumerator DisableInvincibility()
    {
        yield return new WaitForSeconds(3f); // 3초 대기

        // 무적 상태 비활성화
        isInvincible = false;
    }

    private IEnumerator BlinkEffect()
    {
        while (isInvincible)
        {
            // 캐릭터를 보이거나 숨기기
            characterRenderer.enabled = !characterRenderer.enabled;

            // 일정 간격 동안 기다리기
            yield return new WaitForSeconds(blinkInterval);
        }

        // 무적 상태가 해제되면 캐릭터를 다시 보이게 만듦
        characterRenderer.enabled = true;
    }
}