using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public Transform playerTransform;
    public float spawnRadius = 10f; // 나중에 플레이어 화면 밖에서만 생성할지 정해야함
    public void SpawnMonsterNearPlayer()
    {
        Vector3 spawnPosition = playerTransform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 0;
        Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    }
}