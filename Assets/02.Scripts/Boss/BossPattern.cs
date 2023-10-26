using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    public GameObject pillarPrefab; // 기둥 프리팹
    public Transform spawnPoint1; // 기둥1이 생성될 스폰 지역의 Transform
    public Transform spawnPoint2; // 기둥2이 생성될 스폰 지역의 Transform
    public Transform targetTransform1; // 첫 번째 기둥이 박힐 목표 지점의 Transform
    public Transform targetTransform2; // 두 번째 기둥이 박힐 목표 지점의 Transform
    public float moveSpeed = 5.0f; // 기둥의 이동 속도
    public float destroyDelay = 100.0f; // 기둥이 땅에 박힌 후 몇 초 후 제거할 것인지
    public BossController boss; // 보스 몬스터의 참조
    private bool hasPoundedPillars = false;
    private bool isPounded1 = false; // 첫 번째 기둥이 땅에 박혔는지 여부
    private bool isPounded2 = false; // 두 번째 기둥이 땅에 박혔는지 여부


    void Update()
    {
        if (!isPounded1 && !isPounded2 && boss != null && hasPoundedPillars != true)
        {
            PoundPillars();
            hasPoundedPillars = true;
        }
        // 기둥 생성 후의 로직 추가
    }

    void PoundPillars()
    {
        // 첫 번째 기둥 생성 및 초기 위치 설정
        GameObject pillar1 = Instantiate(pillarPrefab, spawnPoint1.position, spawnPoint1.rotation);
        // 두 번째 기둥 생성 및 초기 위치 설정
        GameObject pillar2 = Instantiate(pillarPrefab, spawnPoint2.position, spawnPoint2.rotation);

        // 기둥에 Rigidbody 추가
        Rigidbody pillar1Rigidbody = pillar1.GetComponent<Rigidbody>();
        Rigidbody pillar2Rigidbody = pillar2.GetComponent<Rigidbody>();
        Collider pillar1Collider = pillar1.GetComponent<Collider>();
        Collider pillar2Collider = pillar2.GetComponent<Collider>();
        pillar1Collider.enabled = false;
        pillar2Collider.enabled = false;
        // 기둥을 몇 초 후 제거
        Destroy(pillar1, destroyDelay);
        Destroy(pillar2, destroyDelay);
        // 첫 번째 기둥을 목표 지점으로 날아가도록 설정
        MovePillar(pillar1Rigidbody, pillar1Collider, targetTransform1.position);

        // 두 번째 기둥을 목표 지점으로 날아가도록 설정
        MovePillar(pillar2Rigidbody, pillar2Collider, targetTransform2.position);
    }

    void MovePillar(Rigidbody pillarRigidbody, Collider pillarCollider, Vector3 targetPosition)
    {
        Vector3 moveDirection = (targetPosition - pillarRigidbody.transform.position).normalized;

        // 목표 지점에 도달하면 기둥을 멈추도록 설정
        if (Vector3.Distance(pillarRigidbody.transform.position, targetPosition) < 0.1f)
        {
            pillarRigidbody.velocity = Vector3.zero;
        }
        else
        {
            // 이동 벡터에 이동 속도를 곱하여 힘을 가합니다.
            pillarRigidbody.velocity = moveDirection * moveSpeed;
        }

        // 이동 중인 동안 기둥 콜라이더 비활성화
        pillarCollider.enabled = false;

        // 기둥이 도착하면 콜라이더를 활성화하고 Is Kinematic 비활성화
        StartCoroutine(ActivateCollider(pillarCollider, pillarRigidbody));
    }
    IEnumerator ActivateCollider(Collider pillarCollider, Rigidbody pillarRigidbody)
    {
        yield return new WaitForSeconds(destroyDelay);
        pillarRigidbody.isKinematic = false;
        pillarCollider.enabled = true;
    }
}