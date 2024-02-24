using UnityEngine;
using UnityEngine.Tilemaps;

public class EnvironmentPlacer : MonoBehaviour
{
    public WorldGenerator worldGenerator;
    public Transform environmentParent;
    public LandmarkPlacer landmarkPlacer;
    public Vector3 playerPosition;

    public Tilemap wallTilemap;
    public RuleTile wallTile;
    public RuleTile centralCaveWallTile;
    public Tilemap ceilingTilemap;
    public RuleTile ceilingTile;
    public RuleTile centralCaveCeilingTile;

    public float perlinNoiseScale = 0.01f;
    public int perlinNoiseThreshold = 70;

    void Awake()
    {
        if (landmarkPlacer != null)
        {
            landmarkPlacer.OnLandmarksPlaced += PlaceEnvironment;
        }
    }

    void OnDestroy()
    {
        if (landmarkPlacer != null)
        {
            landmarkPlacer.OnLandmarksPlaced -= PlaceEnvironment;
        }
    }


    void PlaceEnvironment()
    {
        for (int x = 0; x < worldGenerator.mapWidth; x++)
        {
            for (int y = 0; y < worldGenerator.mapHeight; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x - worldGenerator.mapWidth / 2, y - worldGenerator.mapHeight / 2, 0);
                float perlinValue = Mathf.PerlinNoise((x + worldGenerator.seed) * perlinNoiseScale,
                                                      (y + worldGenerator.seed) * perlinNoiseScale) * 100;

                if (perlinValue > perlinNoiseThreshold && !worldGenerator.landmarkPlaced[x, y])
                {
                    wallTilemap.SetTile(tilePosition, wallTile);

                    Vector3Int ceilingPosition = new Vector3Int(tilePosition.x, tilePosition.y + 1, tilePosition.z);
                    if (!wallTilemap.HasTile(ceilingPosition))
                    {
                        ceilingTilemap.SetTile(ceilingPosition, ceilingTile);
                    }
                }

                if (worldGenerator.IsCentralCave(x, y) && ShouldPlaceWall(x, y))
                {
                    wallTilemap.SetTile(tilePosition, centralCaveWallTile);

                    Vector3Int ceilingPosition = new Vector3Int(tilePosition.x, tilePosition.y + 1, tilePosition.z);
                    if (!wallTilemap.HasTile(ceilingPosition))
                    {
                        ceilingTilemap.SetTile(ceilingPosition, centralCaveCeilingTile);
                    }
                }
            }
        }
        for (int x = 0; x < worldGenerator.mapWidth; x++)
        {
            for (int y = 0; y < worldGenerator.mapHeight; y++)
            {
                Vector3Int position = new Vector3Int(x - worldGenerator.mapWidth / 2, y - worldGenerator.mapHeight / 2, 0);
                if (wallTilemap.HasTile(position))
                {
                    if (ceilingTilemap.HasTile(position))
                    {
                        ceilingTilemap.SetTile(position, null);
                    }
                }
            }
        }
    }



    bool ShouldPlaceWall(int x, int y)
    {
        float distanceFromCenter = Mathf.Sqrt((x - worldGenerator.center.x) * (x - worldGenerator.center.x) +
                                              (y - worldGenerator.center.y) * (y - worldGenerator.center.y));

        float distanceFromPlayer = Vector3.Distance(new Vector3(x - worldGenerator.mapWidth / 2,
                                                                y - worldGenerator.mapHeight / 2, 0),
                                                    playerPosition);
        if (distanceFromPlayer <= 50f)
        {
            return false;
        }

        float[] outerRadii = { worldGenerator.centralCircleRadius + 50, worldGenerator.centralCircleRadius + 60 };
        float[] innerRadii = { worldGenerator.centralCircleRadius - 2, worldGenerator.centralCircleRadius - 12 };

        for (int i = 0; i < outerRadii.Length; i++)
        {
            if (distanceFromCenter <= outerRadii[i] && distanceFromCenter >= innerRadii[i])
            {
                return true;
            }
        }
        return false;
    }

}