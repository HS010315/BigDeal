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
    private bool isDie = false;
    public int playerLife = 3;
    public GameObject explosionEffect;
    public Transform respawnPosition;

    private Rigidbody rb;

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
        if (other.CompareTag("Bullet"))     // ź���� �ε����� ��
        {
            Destroy(other.gameObject);      // ź�� �ı�

            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity); // ���� ȿ�� ����
            }
            PlayerDied();
        }
    }
    public void PlayerDied()
    {
        playerLife--;

        gameObject.SetActive(false);
        isDie = true;

        if(playerLife <= 0)
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
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
            canJump = false;
        }
        if(Input.GetKeyDown(KeyCode.L) && !isDashing)
        {
            dashDirection = rb.velocity.normalized;
            StartCoroutine(Dash());

        }
        if (Input.GetKey(KeyCode.LeftShift) && !isFlying)
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
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isFlying = false;
        }
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
        //�뽬 �� ���� ����

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
        Destroy(gameObject);
    }

    private void RespawnPlayer()
    {
        if(isDie)
        {
            gameObject.SetActive(true);
            isDie = false;
        }
        transform.position = respawnPosition.position;

        //������ �� ���� ���� 
    }
}
