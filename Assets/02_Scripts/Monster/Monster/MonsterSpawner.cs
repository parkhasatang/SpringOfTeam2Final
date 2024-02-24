using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public Tilemap wallTilemap;
    private bool playerEnteredZone = false;
    private int maxMonsters = 5;
    private int currentMonsterCount = 0;
    public float spawnRadius = 3f;
    public float minSpawnInterval = 10f;
    public float maxSpawnInterval = 20f;

    private void OnEnable()
    {
        Debug.Log("들어왔다");
        MonsterState.OnMonsterDeath += MonsterRemoved;
    }

    private void OnDisable()
    {
        Debug.Log("나갔다");
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

        int randomIndex = Random.Range(0, monsterPrefabs.Length);

        GameObject selectedPrefab = monsterPrefabs[randomIndex];
        Instantiate(selectedPrefab, spawnPoint, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;
        int attempts = 10;
        while (attempts > 0)
        {
            attempts--;
            Vector3 spawnDirection = Random.insideUnitCircle.normalized;
            spawnDirection.z = spawnDirection.y;
            spawnDirection.y = 0;
            spawnPoint = transform.position + spawnDirection * spawnRadius;

            Vector3Int tilePosition = wallTilemap.WorldToCell(spawnPoint);
            if (!wallTilemap.HasTile(tilePosition))
            {
                return spawnPoint;
            }
        }
        return Vector3.zero;
    }

    public void MonsterRemoved(GameObject monster)
    {
        currentMonsterCount--;
        Debug.Log("감소됐다.");
    }
}