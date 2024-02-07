using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "ScriptableObjects/TileData", order = 1)]
public class TileData : ScriptableObject
{
    [System.Serializable]
    public struct MonsterSpawnData
    {
        public GameObject monsterPrefab;
        public float spawnChance;
    }

    public MonsterSpawnData[] monsters;


    public GameObject GetRandomMonsterPrefab()
    {
        float totalChance = 0;
        foreach (var monster in monsters)
        {
            totalChance += monster.spawnChance;
        }

        float randomPoint = Random.value * totalChance;
        float currentPoint = 0;

        foreach (var monster in monsters)
        {
            currentPoint += monster.spawnChance;
            if (currentPoint >= randomPoint)
            {
                return monster.monsterPrefab;
            }
        }

        return null;
    }
}