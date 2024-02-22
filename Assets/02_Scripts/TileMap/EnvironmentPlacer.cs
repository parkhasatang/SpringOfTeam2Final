using UnityEngine;

public class EnvironmentPlacer : MonoBehaviour
{
    public WorldGenerator worldGenerator;
    public Transform environmentParent;

    public GameObject wallPrefab;

    public GameObject[] forestNaturalObjectPrefabs;
    public GameObject[] seaNaturalObjectPrefabs;
    public GameObject[] dungeonNaturalObjectPrefabs;
    public GameObject[] caveNaturalObjectPrefabs;

    public int placementInterval = 2;

    void Start()
    {
        if (worldGenerator != null)
        {
            worldGenerator.OnGenerationComplete += PlaceWallsAndEnvironment;
        }
    }

    void OnDestroy()
    {
        if (worldGenerator != null)
        {
            worldGenerator.OnGenerationComplete -= PlaceWallsAndEnvironment;
        }
    }

    void PlaceWallsAndEnvironment()
    {
        for (int x = 0; x < worldGenerator.mapWidth; x++)
        {
            for (int y = 0; y < worldGenerator.mapHeight; y++)
            {
                Vector3 position = new Vector3(x - worldGenerator.mapWidth / 2, y - worldGenerator.mapHeight / 2, 0);

                if (worldGenerator.wallPlaced[x, y])
                {
                    Instantiate(wallPrefab, position, Quaternion.identity, environmentParent);
                }
                else if (!worldGenerator.landmarkPlaced[x, y] && Random.Range(0, 100) < 80)
                {
                    if (x % placementInterval == 0 && y % placementInterval == 0)
                    {
                        GameObject[] prefabsToUse = GetPrefabsForTheme(worldGenerator.themeMap[x, y]);
                        if (prefabsToUse.Length > 0)
                        {
                            int randomIndex = Random.Range(0, prefabsToUse.Length);
                            Instantiate(prefabsToUse[randomIndex], position, Quaternion.identity, environmentParent);
                        }
                    }
                }
            }
        }
    }

    GameObject[] GetPrefabsForTheme(int theme)
    {
        switch (theme)
        {
            case 0: return forestNaturalObjectPrefabs;
            case 1: return seaNaturalObjectPrefabs;
            case 2: return dungeonNaturalObjectPrefabs;
            case 3: return caveNaturalObjectPrefabs;
            default: return new GameObject[0];
        }
    }
}