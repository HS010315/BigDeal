using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float DashTime = 0.1f;
    public float DashCoolTime = 5f;
    private bool isDashing = false;
    private Vector3 dashDirection;
    public float jumpForce = 10f;
    private bool canJump = true;
    private bool isFlying = false;
    public int playerLife = 3;

    private Rigidbody rb;

    private Animator ani;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    public void PlayerDied()
    {
        playerLife--;

        if(playerLife <= 0)
        {         
            GameOver();
        }
        else
        {
            RespawnPlayer();
        }

    }



    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
            canJump = false;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            dashDirection = rb.velocity.normalized;
            StartCoroutine(Dash());

        }
        if (Input.GetKeyDown(KeyCode.LeftControl)&& !isFlying)
        {
            isFlying = !isFlying;
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
        //다시 누르면 비활성화
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        float dashTime = 0f;

        while (dashTime < DashTime)
        {
            rb.velocity = dashDirection * dashSpeed;    //점프도 인식이 되서 대쉬 방향이 이상함.
            dashTime += Time.deltaTime;                 
            yield return null;
        }

        rb.velocity = Vector3.zero;
        isDashing = false;

        yield return new WaitForSeconds(DashCoolTime);  //왜 쿨타임 적용이 안되지 ?

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void GameOver()
    {
        //UI 팝업 후 Restart 버튼으로 씬 처음부터 다시 불러옴. + 추가 요소 필요
    }

    private void RespawnPlayer()
    {
        //플레이어가 카메라 속 일정 좌표로 다시 나오게 구현해야함.
    }




}
