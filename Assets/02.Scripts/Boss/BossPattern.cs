using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    public GameObject pillarPrefab; // 기둥 프리팹
    public Vector3 targetPosition1; // 첫 번째 기둥이 박힐 목표 지점
    public Vector3 targetPosition2; // 두 번째 기둥이 박힐 목표 지점
    public float destroyDelay = 1.0f; // 기둥이 땅에 박힌 후 몇 초 후 제거할 것인지
    public BossController boss; // 보스 몬스터의 참조

    private bool isPounded1 = false; // 첫 번째 기둥이 땅에 박혔는지 여부
    private bool isPounded2 = false; // 두 번째 기둥이 땅에 박혔는지 여부
    private BoxCollider boxCollider;

    void Start()
    {
        // 기둥의 Box Collider를 가져와서 비활성화합니다.
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
        // 첫 번째 기둥 생성
        GameObject pillar1 = Instantiate(pillarPrefab, targetPosition1, Quaternion.identity);
        // 두 번째 기둥 생성
        GameObject pillar2 = Instantiate(pillarPrefab, targetPosition2, Quaternion.identity);

        // 기둥을 몇 초 후 제거
        Destroy(pillar1, destroyDelay);
        Destroy(pillar2, destroyDelay);

        isPounded1 = true;
        isPounded2 = true;
    }
}