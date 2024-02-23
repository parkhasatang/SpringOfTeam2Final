using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawnManager : MonoBehaviour
{
    public GameObject logTrapPrefabs;
    private List<GameObject> logTrapPools;
    public List<Transform> spawnPoints;
    public bool isPlayerInside = false;    

    private void Awake()
    {
        logTrapPools = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            GameObject logTrap = Instantiate(logTrapPrefabs,transform);
            logTrap.SetActive(false);
            logTrapPools.Add(logTrap);
        }       
    }

    private void Start()
    {
        StartCoroutine(SpawnLogTrapCoroutine());
    }

    private IEnumerator SpawnLogTrapCoroutine()
    {
        while(true)
        {
            SpawnLogTrap();

            yield return new WaitForSeconds(1f);
        }
    }
   
    public void SpawnLogTrap()
    {
        GameObject _logTrap = null;
        int randomNum = Random.Range(0, 3);
        foreach(GameObject logTrap in logTrapPools)
        {
            if (!logTrap.activeSelf)
            {
                _logTrap = logTrap;
                _logTrap.GetComponent<LogTrap>();
                _logTrap.transform.position = spawnPoints[randomNum].position;
                logTrap.SetActive(true);
                break;
            }
        }

        if (!_logTrap)
        {
            _logTrap = Instantiate(logTrapPrefabs,transform);
            _logTrap.GetComponent<LogTrap>();
            _logTrap.transform.position = spawnPoints[randomNum].position;
            _logTrap.SetActive(true);
        }
    }
    
}
