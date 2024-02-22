using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class WorldGenerator : MonoBehaviour
{
    public Tilemap terrainTilemap;
    public Tile[] caveTiles;
    public Tile[] forestTiles;
    public Tile[] dungeonTiles;
    public Tile[] seaTiles;
    public int mapWidth = 100;
    public int mapHeight = 100;
    public float noiseScale = 0.05f;
    public int[,] themeMap;

    private Vector2Int center;
    private float centralCircleRadius = 20f;
    private float twoThirdsPi = (2f / 3f) * Mathf.PI;

    public event Action OnGenerationComplete;

    void Start()
    {
        themeMap = new int[mapWidth, mapHeight];
        center = new Vector2Int(mapWidth / 2, mapHeight / 2);
        GenerateWorld();
    }


    void GenerateWorld()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Tile tileToPlace = ChooseTile(x, y);
                Vector3Int tilePosition = new Vector3Int(x - center.x, y - center.y, 0);
                terrainTilemap.SetTile(tilePosition, tileToPlace);
            }
        }

        OnGenerationComplete?.Invoke();
    }

    Tile ChooseTile(int x, int y)
    {
        int offsetX = x - center.x;
        int offsetY = y - center.y;

        float distance = Mathf.Sqrt(offsetX * offsetX + offsetY * offsetY);
        float angle = Mathf.Atan2(offsetY, offsetX) + Mathf.PI;
        angle = (angle + twoThirdsPi) % (Mathf.PI * 2);
        if (distance < centralCircleRadius)
        {
            return caveTiles[UnityEngine.Random.Range(0, caveTiles.Length)];
        }
        else if (angle < twoThirdsPi)
        {
            return forestTiles[UnityEngine.Random.Range(0, forestTiles.Length)];
        }
        else if (angle < 2 * twoThirdsPi)
        {
            return seaTiles[UnityEngine.Random.Range(0, seaTiles.Length)];
        }
        else
        {
            return dungeonTiles[UnityEngine.Random.Range(0, dungeonTiles.Length)];
        }
    }
}
