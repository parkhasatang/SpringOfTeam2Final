using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour
{
    public Tilemap terrainTilemap;
    public Tile[] caveTiles;
    public Tile[] forestTiles;
    public Tile[] dungeonTiles;
    public Tile[] seaTiles;

    public int mapWidth = 100;
    public int mapHeight = 100;
    public int[,] themeMap;
    public Vector2Int center;

    public bool[,] landmarkPlaced;
    public bool[,] wallPlaced;
    public float seed;
    private float themeStartAngle;



    public float centralCircleRadius = 50f;
    private float twoThirdsPi = (2f / 3f) * Mathf.PI;
    public float noiseScale = 0.05f;

    public event Action OnGenerationComplete;
    public bool IsCentralCave(int x, int y)
    {
        int offsetX = x - mapWidth / 2;
        int offsetY = y - mapHeight / 2;

        float distanceFromCenter = Mathf.Sqrt(offsetX * offsetX + offsetY * offsetY);
        return distanceFromCenter < centralCircleRadius;
    }
    void Start()
    {
        themeMap = new int[mapWidth, mapHeight];
        landmarkPlaced = new bool[mapWidth, mapHeight];
        wallPlaced = new bool[mapWidth, mapHeight];
        center = new Vector2Int(mapWidth / 2, mapHeight / 2);
        seed = UnityEngine.Random.Range(0f, 9999f);
        InitializeThemeStartAngle();
        GenerateWorld();
    }
    void InitializeThemeStartAngle()
    {
        themeStartAngle = UnityEngine.Random.Range(0f, 360f);
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

                themeMap[x, y] = GetThemeIndex(tileToPlace);
            }
        }

        OnGenerationComplete?.Invoke();
        Debug.Log("생성완료");
    }

    Tile ChooseTile(int x, int y)
    {
        int offsetX = x - center.x;
        int offsetY = y - center.y;

        float distanceFromCenter = Mathf.Sqrt(offsetX * offsetX + offsetY * offsetY);
        float angleFromCenter = Mathf.Atan2(offsetY, offsetX) * Mathf.Rad2Deg;
        angleFromCenter = (angleFromCenter + 360f) % 360f;
        angleFromCenter = (angleFromCenter - themeStartAngle + 360f) % 360f;

        if (distanceFromCenter < centralCircleRadius)
        {
            return caveTiles[UnityEngine.Random.Range(0, caveTiles.Length)];
        }
        else
        {
            float section = 360f / 3f;
            if (angleFromCenter < section)
            {
                return forestTiles[UnityEngine.Random.Range(0, forestTiles.Length)];
            }
            else if (angleFromCenter < section * 2)
            {
                return seaTiles[UnityEngine.Random.Range(0, seaTiles.Length)];
            }
            else
            {
                return dungeonTiles[UnityEngine.Random.Range(0, dungeonTiles.Length)];
            }
        }
    }
    int GetThemeIndex(Tile tile)
    {
        if (Array.Exists(caveTiles, element => element == tile))
            return 0;
        else if (Array.Exists(forestTiles, element => element == tile))
            return 1;
        else if (Array.Exists(seaTiles, element => element == tile))
            return 2;
        else if (Array.Exists(dungeonTiles, element => element == tile))
            return 3;

        return -1;
    }
}
