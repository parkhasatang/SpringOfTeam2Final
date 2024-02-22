using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour
{
    public Tilemap terrainTilemap;
    public Tile caveTile;
    public Tile forestTile;
    public Tile dungeonTile;
    public Tile seaTile;
    public Tile graveyardTile;
    public int mapWidth = 100;
    public int mapHeight = 100;
    public int[,] themeMap;
    private Dictionary<string, int> directionalThemes;
    private Vector2Int center;

    public float noiseScale = 0.05f;
    private float centralCircleRadius = 20f;


    public event Action OnGenerationComplete;

    void Start()
    {
        themeMap = new int[mapWidth, mapHeight];
        AssignDirectionalThemes();
        GenerateWorld();
    }

    void AssignDirectionalThemes()
    {
        List<int> themes = new List<int> { 0, 1, 2, 3 };
        themes.Shuffle();

        directionalThemes = new Dictionary<string, int>
        {
            {"North", themes[0]},
            {"East", themes[1]},
            {"South", themes[2]},
            {"West", themes[3]}
        };
    }

    void GenerateWorld()
    {
        Vector2 forestOffset = new Vector2(10000, 10000);
        Vector2 seaOffset = new Vector2(30000, 10000);
        Vector2 dungeonOffset = new Vector2(10000, 30000);
        Vector2 graveyardOffset = new Vector2(30000, 30000);

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float xCoord = x * noiseScale;
                float yCoord = y * noiseScale;

                float forestNoise = Mathf.PerlinNoise(xCoord + forestOffset.x, yCoord + forestOffset.y);
                float seaNoise = Mathf.PerlinNoise(xCoord + seaOffset.x, yCoord + seaOffset.y);
                float dungeonNoise = Mathf.PerlinNoise(xCoord + dungeonOffset.x, yCoord + dungeonOffset.y);
                float graveyardNoise = Mathf.PerlinNoise(xCoord + graveyardOffset.x, yCoord + graveyardOffset.y);

                float maxNoise = Mathf.Max(forestNoise, seaNoise, dungeonNoise, graveyardNoise);
                Tile tileToPlace;

                if (maxNoise == forestNoise)
                {
                    tileToPlace = forestTile;
                }
                else if (maxNoise == seaNoise)
                {
                    tileToPlace = seaTile;
                }
                else if (maxNoise == dungeonNoise)
                {
                    tileToPlace = dungeonTile;
                }
                else
                {
                    tileToPlace = graveyardTile;
                }
                terrainTilemap.SetTile(new Vector3Int(x - mapWidth / 2, y - mapHeight / 2, 0), tileToPlace);
            }
        }

        OnGenerationComplete?.Invoke();
    }
}

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}