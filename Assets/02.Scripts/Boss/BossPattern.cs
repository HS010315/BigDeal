using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    public GameObject pillarPrefab; // ��� ������
    public Vector3 targetPosition1; // ù ��° ����� ���� ��ǥ ����
    public Vector3 targetPosition2; // �� ��° ����� ���� ��ǥ ����
    public float destroyDelay = 1.0f; // ����� ���� ���� �� �� �� �� ������ ������
    public BossController boss; // ���� ������ ����

    private bool isPounded1 = false; // ù ��° ����� ���� �������� ����
    private bool isPounded2 = false; // �� ��° ����� ���� �������� ����
    private BoxCollider boxCollider;

    void Start()
    {
        // ����� Box Collider�� �����ͼ� ��Ȱ��ȭ�մϴ�.
        boxCollider = GetComponent <BoxCollider>();
        boxCollider.enabled = false;
    }

    void Update()
    {
        if (!isPounded1 && !isPounded2 && boss != null && boss.GetCurrentHealth() <= boss.GetMaxHealth() * 0.5f)
        {
            PoundPillars();
        }
    }

    void PoundPillars()
    {
        // ù ��° ��� ����
        GameObject pillar1 = Instantiate(pillarPrefab, targetPosition1, Quaternion.identity);
        // �� ��° ��� ����
        GameObject pillar2 = Instantiate(pillarPrefab, targetPosition2, Quaternion.identity);

        // ����� �� �� �� ����
        Destroy(pillar1, destroyDelay);
        Destroy(pillar2, destroyDelay);

        isPounded1 = true;
        isPounded2 = true;
    }
}