using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public GameObject bossMonsterPrefab;
    private bool playerEnteredZone = false;
    private int maxMonsters = 4;
    private int currentMonsterCount = 0;
    private int slimeDeathsCount = 0;
    private bool bossSpawned = false;
    public float spawnRadius = 4f;
    public float minSpawnInterval = 10f;
    public float maxSpawnInterval = 20f;

    private Queue<GameObject> monsterPool = new Queue<GameObject>();
    private GameObject bossInstance;
    private Coroutine spawningCoroutine;

    private void Start()
    {
        InitializeMonsterPool();
        InitializeBossMonster();
    }

    private void InitializeMonsterPool()
    {
        for (int i = 0; i < maxMonsters * 2; i++)
        {
            GameObject monster = Instantiate(monsterPrefab);
            monster.SetActive(false);
            monsterPool.Enqueue(monster);
        }
    }

    private void InitializeBossMonster()
    {
        bossInstance = Instantiate(bossMonsterPrefab);
        bossInstance.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEnteredZone = true;
            if (spawningCoroutine == null)
            {
                spawningCoroutine = StartCoroutine(SpawnMonstersWithInterval());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEnteredZone = false;
        }
    }

    private IEnumerator SpawnMonstersWithInterval()
    {
        while (playerEnteredZone)
        {
            if (currentMonsterCount < maxMonsters)
            {
                SpawnMonsterFromPool();
            }

            if (slimeDeathsCount >= 5 && !bossSpawned)
            {
                SpawnBossMonster();
            }

            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    private void SpawnMonsterFromPool()
    {
        if (monsterPool.Count > 0)
        {
            GameObject monster = monsterPool.Dequeue();
            monster.transform.position = GetRandomSpawnPoint();
            monster.SetActive(true);
            currentMonsterCount++;
        }
    }

    private void SpawnBossMonster()
    {
        if (!bossSpawned && slimeDeathsCount >= 5)
        {
            bossInstance.transform.position = GetRandomSpawnPoint();
            bossInstance.SetActive(true);
            bossSpawned = true;
            Debug.Log("쿵쿵거리는 소리가 들립니다.");
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPoint = transform.position + (Vector3)spawnDirection * spawnRadius;
        return spawnPoint;
    }

    public void MonsterRemoved()
    {
        currentMonsterCount--;
        slimeDeathsCount++;

        if (slimeDeathsCount >= 5 && bossSpawned)
        {
            bossSpawned = false;
        }

        if (playerEnteredZone && currentMonsterCount < maxMonsters && spawningCoroutine == null)
        {
            spawningCoroutine = StartCoroutine(SpawnMonstersWithInterval());
        }
    }
}