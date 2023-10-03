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
        //������ ���� �Ҹ� �� �߶�
        //�ٽ� ������ ��Ȱ��ȭ
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        float dashTime = 0f;

        while (dashTime < DashTime)
        {
            rb.velocity = dashDirection * dashSpeed;    //������ �ν��� �Ǽ� �뽬 ������ �̻���.
            dashTime += Time.deltaTime;                 
            yield return null;
        }

        rb.velocity = Vector3.zero;
        isDashing = false;

        yield return new WaitForSeconds(DashCoolTime);  //�� ��Ÿ�� ������ �ȵ��� ?

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
        //UI �˾� �� Restart ��ư���� �� ó������ �ٽ� �ҷ���. + �߰� ��� �ʿ�
    }

    private void RespawnPlayer()
    {
        //�÷��̾ ī�޶� �� ���� ��ǥ�� �ٽ� ������ �����ؾ���.
    }




}
