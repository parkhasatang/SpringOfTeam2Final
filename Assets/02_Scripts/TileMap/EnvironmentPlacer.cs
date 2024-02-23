using UnityEngine;
using UnityEngine.Tilemaps;

public class EnvironmentPlacer : MonoBehaviour
{
    public WorldGenerator worldGenerator;
    public Transform environmentParent;

    public Tilemap wallTilemap;
    public RuleTile wallTile;
    public Tilemap ceilingTilemap;
    public RuleTile ceilingTile;

    public float perlinNoiseScale = 0.1f;
    public float naturalObjectNoiseScale = 0.3f;
    public int perlinNoiseThreshold = 70;

    public GameObject[] forestNaturalObjectPrefabs;
    public GameObject[] seaNaturalObjectPrefabs;
    public GameObject[] dungeonNaturalObjectPrefabs;
    public GameObject[] caveNaturalObjectPrefabs;

    public int naturalObjectPlacementInterval = 3;

    void Awake()
    {
        if (worldGenerator != null)
        {
            worldGenerator.OnGenerationComplete += PlaceEnvironment;
        }
    }

    void OnDestroy()
    {
        if (worldGenerator != null)
        {
            worldGenerator.OnGenerationComplete -= PlaceEnvironment;
        }
    }

    void PlaceEnvironment()
    {
        for (int x = 0; x < worldGenerator.mapWidth; x++)
        {
            for (int y = 0; y < worldGenerator.mapHeight; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x - worldGenerator.mapWidth / 2, y - worldGenerator.mapHeight / 2, 0);

                float perlinValue = Mathf.PerlinNoise(x * perlinNoiseScale + worldGenerator.seed, y * perlinNoiseScale + worldGenerator.seed) * 100;
                bool placeWall = perlinValue > perlinNoiseThreshold && !worldGenerator.landmarkPlaced[x, y];
                if (placeWall)
                {
                    wallTilemap.SetTile(tilePosition, wallTile);


                    Vector3Int ceilingPosition = new Vector3Int(tilePosition.x, tilePosition.y + 1, tilePosition.z);
                    if (!wallTilemap.HasTile(ceilingPosition))
                    {
                        ceilingTilemap.SetTile(ceilingPosition, ceilingTile);
                    }
                }

                if (!placeWall && !worldGenerator.landmarkPlaced[x, y] && (x % naturalObjectPlacementInterval == 0 && y % naturalObjectPlacementInterval == 0))
                {
                    float noiseValue = Mathf.PerlinNoise(x * naturalObjectNoiseScale, y * naturalObjectNoiseScale);
                    if (noiseValue > 0.5)
                    {
                        PlaceNaturalObject(x, y);
                    }
                }
            }
        }
    }

    void PlaceNaturalObject(int x, int y)
    {
        Vector3 position = new Vector3(x - worldGenerator.mapWidth / 2, 0, y - worldGenerator.mapHeight / 2);
        GameObject[] prefabsToUse = GetPrefabsForTheme(worldGenerator.themeMap[x, y]);
        if (prefabsToUse.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, prefabsToUse.Length);
            Instantiate(prefabsToUse[randomIndex], position, Quaternion.identity, environmentParent);
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