using System.Collections;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public GameObject enemyPrefab; // Enemy ������ ����
    public float spawnInterval = 3f; // Enemy ���� ���� (��)

    public int playerScore = 0; // �÷��̾� ���ھ� ���

    public void IncreaseScore(int amount) // �Լ��� ���ؼ� ���������ش�
    {
        playerScore += amount;
    }

    void Start()
    {
        // ���� �������� SpawnEnemy �Լ��� ȣ��
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Enemy�� ���� ī�޶� �������� ��Ÿ���� ����
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        float spawnX = mainCamera.transform.position.x + cameraWidth / 2f + 2f;
        float spawnY = Random.Range(mainCamera.transform.position.y - cameraHeight / 2f, mainCamera.transform.position.y + cameraHeight / 2f) + 8f;
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}