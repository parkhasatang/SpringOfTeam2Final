using UnityEngine;
using System.Collections;


public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
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
            Instantiate(monsterPrefab, spawnPoint, Quaternion.identity);
            currentMonsterCount++;
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 spawnDirection = Random.onUnitSphere;
        spawnDirection.y = 0;
        Vector3 spawnPoint = transform.position + spawnDirection * spawnRadius;
        return spawnPoint;
    }

    public void MonsterRemoved(GameObject monster)
    {
        currentMonsterCount--;
        Debug.Log("감소됐다.");
    }
}