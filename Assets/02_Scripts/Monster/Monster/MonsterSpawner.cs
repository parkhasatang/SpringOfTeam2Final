using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public Tilemap wallTilemap;
    public Tilemap groundTilemap;

    private bool playerEnteredZone = false;
    private int maxMonsters = 5;
    private int currentMonsterCount = 0;
    public float spawnRadius = 3f;
    public float minSpawnInterval = 10f;
    public float maxSpawnInterval = 20f;

    private void OnEnable()
    {
        MonsterState.OnMonsterDeath += MonsterRemoved;
    }

    private void OnDisable()
    {
        MonsterState.OnMonsterDeath -= MonsterRemoved;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEnteredZone = true;
            StartCoroutine(SpawnMonstersWithInterval());
        }
    }

    private IEnumerator SpawnMonstersWithInterval()
    {
        while (playerEnteredZone && currentMonsterCount < maxMonsters)
        {
            Vector3 spawnPoint = GetRandomSpawnPoint();
            if (spawnPoint != Vector3.zero)
            {
                InstantiateRandomMonster(spawnPoint);
                currentMonsterCount++;
            }
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    private void InstantiateRandomMonster(Vector3 spawnPoint)
    {
        if (monsterPrefabs == null || monsterPrefabs.Length == 0)
        {
            Debug.LogWarning("Monster prefabs are not assigned.");
            return;
        }

        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject selectedPrefab = monsterPrefabs[randomIndex];
        Instantiate(selectedPrefab, spawnPoint, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;
        int attempts = 100;
        while (attempts > 0)
        {
            attempts--;
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 potentialSpawnPoint = transform.position + new Vector3(spawnDirection.x, 0, spawnDirection.y);

            Vector3Int groundTilePosition = groundTilemap.WorldToCell(potentialSpawnPoint);
            Vector3Int wallTilePosition = wallTilemap.WorldToCell(potentialSpawnPoint);

            if (groundTilemap.HasTile(groundTilePosition) && !wallTilemap.HasTile(wallTilePosition))
            {
                return groundTilemap.CellToWorld(groundTilePosition) + new Vector3(0.5f, 0.5f, 0);
            }
        }
        return Vector3.zero;
    }

    public void MonsterRemoved(GameObject monster)
    {
        currentMonsterCount--;
        Debug.Log("°¨¼ÒµÆ´Ù.");
    }
}