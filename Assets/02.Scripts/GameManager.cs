using System.Collections;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public GameObject enemyPrefab; // Enemy ������ ����
    public float spawnInterval = 3f; // Enemy ���� ���� (��)
    float initialDelay = 2.0f;
    public int playerScore = 0; // �÷��̾� ���ھ� ���
    private bool bossInCameraView = false;

    public void IncreaseScore(int amount) // �Լ��� ���ؼ� ���������ش�
    {
        playerScore += amount;
    }

    void Start()
    {
        bossInCameraView = false;

        // ���� �������� SpawnEnemy �Լ��� ȣ��
        InvokeRepeating("SpawnEnemy", initialDelay, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Enemy�� ���� ī�޶� �������� ��Ÿ���� ����
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
        // ī�޶� ������ ���̴��� Ȯ��
        if (!bossInCameraView)
        {
            CheckForBossInCameraView();
        }
    }

    void CheckForBossInCameraView()
    {
        // ���� ������Ʈ�� ã�ų� �±׸� �̿��Ͽ� Ž��
        GameObject bossObject = GameObject.FindGameObjectWithTag("Boss");

        if (bossObject != null)
        {
            // ī�޶��� �þ� ������ ����Ͽ� ������ ��ġ�� üũ
            Camera mainCamera = Camera.main;
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(bossObject.transform.position);

            if (viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1)
            {
                // ������ ī�޶� �þ߿� �������� �� ������ ����
                bossInCameraView = true;
                CancelInvoke("SpawnEnemy");
            }
        }
    }
}