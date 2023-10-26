using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    public GameObject pillarPrefab; // ��� ������
    public Transform spawnPoint1; // ���1�� ������ ���� ������ Transform
    public Transform spawnPoint2; // ���2�� ������ ���� ������ Transform
    public Transform targetTransform1; // ù ��° ����� ���� ��ǥ ������ Transform
    public Transform targetTransform2; // �� ��° ����� ���� ��ǥ ������ Transform
    public float moveSpeed = 5.0f; // ����� �̵� �ӵ�
    public float destroyDelay = 100.0f; // ����� ���� ���� �� �� �� �� ������ ������
    public BossController boss; // ���� ������ ����
    private bool hasPoundedPillars = false;
    private bool isPounded1 = false; // ù ��° ����� ���� �������� ����
    private bool isPounded2 = false; // �� ��° ����� ���� �������� ����


    void Update()
    {
        if (!isPounded1 && !isPounded2 && boss != null && hasPoundedPillars != true)
        {
            PoundPillars();
            hasPoundedPillars = true;
        }
        // ��� ���� ���� ���� �߰�
    }

    void PoundPillars()
    {
        // ù ��° ��� ���� �� �ʱ� ��ġ ����
        GameObject pillar1 = Instantiate(pillarPrefab, spawnPoint1.position, spawnPoint1.rotation);
        // �� ��° ��� ���� �� �ʱ� ��ġ ����
        GameObject pillar2 = Instantiate(pillarPrefab, spawnPoint2.position, spawnPoint2.rotation);

        // ��տ� Rigidbody �߰�
        Rigidbody pillar1Rigidbody = pillar1.GetComponent<Rigidbody>();
        Rigidbody pillar2Rigidbody = pillar2.GetComponent<Rigidbody>();
        Collider pillar1Collider = pillar1.GetComponent<Collider>();
        Collider pillar2Collider = pillar2.GetComponent<Collider>();
        pillar1Collider.enabled = false;
        pillar2Collider.enabled = false;
        // ����� �� �� �� ����
        Destroy(pillar1, destroyDelay);
        Destroy(pillar2, destroyDelay);
        // ù ��° ����� ��ǥ �������� ���ư����� ����
        MovePillar(pillar1Rigidbody, pillar1Collider, targetTransform1.position);

        // �� ��° ����� ��ǥ �������� ���ư����� ����
        MovePillar(pillar2Rigidbody, pillar2Collider, targetTransform2.position);
    }

    void MovePillar(Rigidbody pillarRigidbody, Collider pillarCollider, Vector3 targetPosition)
    {
        Vector3 moveDirection = (targetPosition - pillarRigidbody.transform.position).normalized;

        // ��ǥ ������ �����ϸ� ����� ���ߵ��� ����
        if (Vector3.Distance(pillarRigidbody.transform.position, targetPosition) < 0.1f)
        {
            pillarRigidbody.velocity = Vector3.zero;
        }
        else
        {
            // �̵� ���Ϳ� �̵� �ӵ��� ���Ͽ� ���� ���մϴ�.
            pillarRigidbody.velocity = moveDirection * moveSpeed;
        }

        // �̵� ���� ���� ��� �ݶ��̴� ��Ȱ��ȭ
        pillarCollider.enabled = false;

        // ����� �����ϸ� �ݶ��̴��� Ȱ��ȭ�ϰ� Is Kinematic ��Ȱ��ȭ
        StartCoroutine(ActivateCollider(pillarCollider, pillarRigidbody));
    }
    IEnumerator ActivateCollider(Collider pillarCollider, Rigidbody pillarRigidbody)
    {
        yield return new WaitForSeconds(destroyDelay);
        pillarRigidbody.isKinematic = false;
        pillarCollider.enabled = true;
    }
}