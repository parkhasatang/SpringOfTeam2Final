using UnityEngine;
using UnityEngine.Tilemaps;

public class EnvironmentPlacer : MonoBehaviour
{
    public WorldGenerator worldGenerator;
    public Transform environmentParent;
    public LandmarkPlacer landmarkPlacer;
    public Vector3 playerPosition; // 플레이어의 위치

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
        // Convert player position to Tilemap grid coordinates
        Vector3Int playerTilemapPosition = wallTilemap.WorldToCell(playerPosition);

        for (int x = 0; x < worldGenerator.mapWidth; x++)
        {
            for (int y = 0; y < worldGenerator.mapHeight; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x - worldGenerator.mapWidth / 2, y - worldGenerator.mapHeight / 2, 0);
                // Check the distance from the player in Tilemap grid coordinates
                if (Vector3Int.Distance(playerTilemapPosition, tilePosition) <= 5f)
                {
                    continue; // Skip if within 5 units of the player
                }

                if (!worldGenerator.landmarkPlaced[x, y])
                {
                    float perlinValue = Mathf.PerlinNoise((x + worldGenerator.seed) * perlinNoiseScale, (y + worldGenerator.seed) * perlinNoiseScale) * 100;

                    if (perlinValue > perlinNoiseThreshold)
                    {
                        wallTilemap.SetTile(tilePosition, wallTile);
                        Vector3Int ceilingPosition = new Vector3Int(tilePosition.x, tilePosition.y + 1, tilePosition.z);
                        if (!wallTilemap.HasTile(ceilingPosition))
                        {
                            ceilingTilemap.SetTile(ceilingPosition, ceilingTile);
                        }
                    }
                }

                if (worldGenerator.IsCentralCave(x, y) && ShouldPlaceWall(x, y))
                {
                    if (!worldGenerator.landmarkPlaced[x, y])
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
        }
        for (int x = 0; x < worldGenerator.mapWidth; x++)
        {
            for (int y = 0; y < worldGenerator.mapHeight; y++)
            {
                Vector3Int position = new Vector3Int(x - worldGenerator.mapWidth / 2, y - worldGenerator.mapHeight / 2, 0);
                if (wallTilemap.HasTile(position) && ceilingTilemap.HasTile(position))
                {
                    ceilingTilemap.SetTile(position, null);
                }
            }
        }
    }

    bool ShouldPlaceWall(int x, int y)
    {
        float distanceFromCenter = Mathf.Sqrt((x - worldGenerator.center.x) * (x - worldGenerator.center.x) + (y - worldGenerator.center.y) * (y - worldGenerator.center.y));

        Vector2 playerTilePosition = new Vector2(playerPosition.x + worldGenerator.mapWidth / 2, playerPosition.y + worldGenerator.mapHeight / 2);

        float distanceFromPlayer = Vector2.Distance(new Vector2(x, y), playerTilePosition);

        if (distanceFromPlayer <= 5f)
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