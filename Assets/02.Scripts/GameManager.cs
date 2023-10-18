using System.Collections;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public GameObject enemyPrefab; // Enemy 프리팹 설정
    public float spawnInterval = 3f; // Enemy 생성 간격 (초)
    float initialDelay = 2.0f;
    public int playerScore = 0; // 플레이어 스코어 등록
    private bool bossInCameraView = false;

    public void IncreaseScore(int amount) // 함수를 통해서 증가시켜준다
    {
        playerScore += amount;
    }

    void Start()
    {
        bossInCameraView = false;

        // 일정 간격으로 SpawnEnemy 함수를 호출
        InvokeRepeating("SpawnEnemy", initialDelay, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Enemy를 메인 카메라 우측에서 나타나게 생성
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        float spawnX = mainCamera.transform.position.x + cameraWidth / 2f + 2f;
        float spawnY = Random.Range(mainCamera.transform.position.y - cameraHeight / 4f, mainCamera.transform.position.y + cameraHeight / 4f) + 1f;
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    void Update()
    {
        // 카메라에 보스가 보이는지 확인
        if (!bossInCameraView)
        {
            CheckForBossInCameraView();
        }
    }

    void CheckForBossInCameraView()
    {
        // 보스 오브젝트를 찾거나 태그를 이용하여 탐지
        GameObject bossObject = GameObject.FindGameObjectWithTag("Boss");

        if (bossObject != null)
        {
            // 카메라의 시야 영역을 고려하여 보스의 위치를 체크
            Camera mainCamera = Camera.main;
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(bossObject.transform.position);

            if (viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1)
            {
                // 보스가 카메라 시야에 들어왔으면 적 생성을 중지
                bossInCameraView = true;
                CancelInvoke("SpawnEnemy");
            }
        }
    }
}