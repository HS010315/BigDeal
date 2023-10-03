using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player; 
    private Rigidbody rb;
    private bool shouldMove = true;
    public float moveSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (shouldMove && player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.Normalize();
            rb.velocity = directionToPlayer * moveSpeed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MainCamera"))
        {
            shouldMove = false;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }
}