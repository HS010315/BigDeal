using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    public Vector3 targetPosition; // ����� ���� ��ǥ ����
    public float destroyDelay = 1.0f; // ����� ���� ���� �� �� �� �� ������ ������
    public BossController boss; // ���� ������ ����

    private bool isPounded = false; // ����� ���� �������� ����
    private BoxCollider boxCollider;

    void Start()
    {
        // ����� Box Collider�� �����ͼ� ��Ȱ��ȭ�մϴ�.
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    void Update()
    {
        if (!isPounded && boss != null && boss.GetCurrentHealth() <= boss.GetMaxHealth() * 0.5f)
        {
            PoundPillar();
        }
    }

    void PoundPillar()
    {
        // ����� ��ǥ ������ �ھ����� ǥ��
        transform.position = targetPosition;
        isPounded = true;

        // ����� �� �� �� ����
        Destroy(gameObject, destroyDelay);
    }
}