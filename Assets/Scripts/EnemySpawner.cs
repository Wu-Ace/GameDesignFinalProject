using Unity.XR.OpenVR;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 敌人预制体
    public float minSpawnDelay = 0.5f; // 最小生成延迟
    public float maxSpawnDelay = 2f; // 最大生成延迟

    private Camera mainCamera;
    private Vector3 viewportPoint;

    private void Start()
    {
        mainCamera = Camera.main;
        // 开始生成敌人
        Invoke("SpawnEnemy", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomSpawnPositionOutsideCamera();
        // 生成敌人
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        // 设置下一次生成敌人的延迟时间
        Invoke("SpawnEnemy", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    Vector3 GetRandomSpawnPositionOutsideCamera()
    {
        
        Vector3 cameraPosition = mainCamera.transform.position;// 获取摄像机视野边界
        
        if (Random.Range(0, 2) == 0)
        {
            viewportPoint.x = Random.Range(1.5f, 2.0f);
        }
        else
        {
            viewportPoint.x = Random.Range(-2.0f, -1.5f);
        }
        if (Random.Range(0, 2) == 0)
        {
            viewportPoint.z = mainCamera.nearClipPlane + 10f;
        }
        else
        {
            viewportPoint.z = mainCamera.nearClipPlane - 10f;
        }
       
        Vector3 spawnPos = mainCamera.ViewportToWorldPoint(viewportPoint); // 将视口坐标转换为世界坐标
        spawnPos.y = 0f; // 确保在地面上
        return spawnPos;
    }
}