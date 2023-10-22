using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    public Vector3 targetPosition; // 기둥이 박힐 목표 지점
    public float destroyDelay = 1.0f; // 기둥이 땅에 박힌 후 몇 초 후 제거할 것인지
    public BossController boss; // 보스 몬스터의 참조

    private bool isPounded = false; // 기둥이 땅에 박혔는지 여부
    private BoxCollider boxCollider;

    void Start()
    {
        // 기둥의 Box Collider를 가져와서 비활성화합니다.
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
        // 기둥을 목표 지점에 박았음을 표시
        transform.position = targetPosition;
        isPounded = true;

        // 기둥을 몇 초 후 제거
        Destroy(gameObject, destroyDelay);
    }
}