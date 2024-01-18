using System;
using Unity.XR.OpenVR;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1; // 敌人预制体1
    public GameObject enemyPrefab2; // 敌人预制体2
    public GameObject enemyPrefab3; // 敌人预制体3
    public float minSpawnDelay = 0.5f; // 最小生成延迟
    public float maxSpawnDelay = 2f; // 最大生成延迟

    private Camera mainCamera;
    private Vector3 viewportPoint;
    public Transform playerTransform;

    private void Start()
    {
        mainCamera = Camera.main;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // 开始生成敌人
        Invoke("SpawnEnemy", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    private void Update()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomSpawnPositionAroundPlayer();
        // 生成敌人
        int rand = Random.Range(1, 11); // 生成 1 到 10 的随机数
        GameObject enemyPrefab;

        // 根据不同的随机数范围生成不同类型的敌人
        if (rand <= 5) // 50% 的概率
        {
            enemyPrefab = enemyPrefab1;
        }
        else if (rand <= 9) // 40% 的概率
        {
            enemyPrefab = enemyPrefab2;
        }
        else // 10% 的概率
        {
            enemyPrefab = enemyPrefab3;
        }

        // 生成敌人
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        // 设置下一次生成敌人的延迟时间
        Invoke("SpawnEnemy", Random.Range(minSpawnDelay, maxSpawnDelay));

    }

    Vector3 GetRandomSpawnPositionAroundPlayer()
    {
        Vector3 playerPosition = playerTransform.position; // 获取玩家的位置

        float spawnRadius = 40f; // 设置生成半径，可以根据需要调整

        // 在玩家周围生成随机位置
        float randomAngle = Random.Range(0f, 360f);
        float spawnX = playerPosition.x + spawnRadius * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
        float spawnZ = playerPosition.z + spawnRadius * Mathf.Sin(randomAngle * Mathf.Deg2Rad);

        Vector3 spawnPos = new Vector3(spawnX, 0f, spawnZ);
        return spawnPos;
    }

}