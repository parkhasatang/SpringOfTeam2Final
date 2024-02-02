using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    private bool playerEnteredZone = false;
    private int maxMonsters = 4;
    private int currentMonsterCount = 0;
    public float spawnRadius = 4f;
    public float minSpawnInterval = 10f;
    public float maxSpawnInterval = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEnteredZone = true;
            Debug.Log("µé¾î¿È");
            StartSpawning();
        }
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnMonstersWithInterval());
    }

    private IEnumerator SpawnMonstersWithInterval()
    {
        while (playerEnteredZone && currentMonsterCount < maxMonsters)
        {
            Vector3 spawnPoint = GetRandomSpawnPoint();
            Instantiate(monsterPrefab, spawnPoint, Quaternion.identity);
            currentMonsterCount++;
            Debug.Log("¼ÒÈ¯µÆ´Ù");

            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 spawnDirection = Random.onUnitSphere;
        spawnDirection.y = 0;
        Vector3 spawnPoint = transform.position + spawnDirection * spawnRadius;
        return spawnPoint;
    }

    public void MonsterRemoved()
    {
        currentMonsterCount--;
    }
}