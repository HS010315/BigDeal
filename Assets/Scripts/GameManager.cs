using System.Collections;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public GameObject enemyPrefab; // Enemy 프리팹 설정
    public float spawnInterval = 3f; // Enemy 생성 간격 (초)

    public int playerScore = 0; // 플레이어 스코어 등록

    public void IncreaseScore(int amount) // 함수를 통해서 증가시켜준다
    {
        playerScore += amount;
    }

    void Start()
    {
        // 일정 간격으로 SpawnEnemy 함수를 호출
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Enemy를 메인 카메라 우측에서 나타나게 생성
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        float spawnX = mainCamera.transform.position.x + cameraWidth / 2f + 2f;
        float spawnY = Random.Range(mainCamera.transform.position.y - cameraHeight / 2f, mainCamera.transform.position.y + cameraHeight / 2f) + 8f;
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}