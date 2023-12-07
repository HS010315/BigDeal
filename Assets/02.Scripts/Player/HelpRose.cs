using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpRose : MonoBehaviour
{

    public GameObject woodColumnPrefab;
    public Transform[] dropPosition;
    public float fallingSpeed = 5f;
    public GameObject Petrose;
    public Animator roseani;

    public float coolDown = 10f;
    private float lastDropTime;

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            Petrose.SetActive(true);
        }
        else
        {
            Petrose.SetActive(false);
        }

        lastDropTime = -coolDown;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J) && Time.time - lastDropTime >= coolDown)
        {
            DropWoodColumn();
            roseani.SetTrigger("Attack");
            lastDropTime = Time.time;
        }
    }

    void DropWoodColumn()
    {
        foreach (Transform dropPosition in dropPosition)
        {
            GameObject woodColumn = Instantiate(woodColumnPrefab, dropPosition.position, Quaternion.identity);

            Rigidbody rb = woodColumn.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = woodColumn.AddComponent<Rigidbody>();
            }
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.velocity = Vector3.down * fallingSpeed;
            Destroy(woodColumn, 3f);
        }

    }
}