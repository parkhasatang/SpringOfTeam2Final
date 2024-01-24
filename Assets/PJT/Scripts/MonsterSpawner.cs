using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public Transform playerTransform;
    public Camera mainCamera;
    public float spawnRadius = 10f;
    public int maxMonsters = 5;
    private float spawnDelay = 5f;
    private float lastSpawnTime;

    private void Update()
    {
        if (Time.time > lastSpawnTime + spawnDelay && CountMonsters() < maxMonsters)
        {
            SpawnMonsterNearPlayer();
            lastSpawnTime = Time.time;
        }
    }
    private int CountMonsters()
    {
        return GameObject.FindGameObjectsWithTag("Monster").Length;
    }

    private void SpawnMonsterNearPlayer()
    {
        Vector3 spawnDirection = Random.onUnitSphere;
        spawnDirection.y = 0;
        Vector3 spawnPoint = playerTransform.position + spawnDirection * spawnRadius;
        Debug.Log("»ý¼º");
        Instantiate(monsterPrefab, spawnPoint, Quaternion.identity);
    }
}
