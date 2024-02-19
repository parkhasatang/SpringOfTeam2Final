using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class WorldGenerator : MonoBehaviour
{
    public Tilemap terrainTilemap;
    public Tile[] forestTiles; // 숲 지역용 타일
    public Tile[] caveTiles; // 동굴 지역용 타일
    public int[,] themeMap;

    public int mapWidth = 100;
    public int mapHeight = 100;
    public float noiseScale = 0.3f;
    public float themeScale = 0.05f;

    public event Action OnGenerationComplete;

    void Start()
    {
        GenerateWorld();
        OnGenerationComplete?.Invoke();
    }

    void GenerateWorld()
    {
        themeMap = new int[mapWidth, mapHeight];

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float elevation = Mathf.PerlinNoise(x * noiseScale, y * noiseScale);
                float themeNoise = Mathf.PerlinNoise(x * themeScale + 10000, y * themeScale + 10000);

                Tile[] tileArray;
                if (themeNoise < 0.5) // 숲 지역
                {
                    tileArray = forestTiles;
                    themeMap[x, y] = 0;
                }
                else // 동굴 지역
                {
                    tileArray = caveTiles;
                    themeMap[x, y] = 1;
                }

                int tileIndex = Mathf.FloorToInt(elevation * tileArray.Length);
                tileIndex = Mathf.Clamp(tileIndex, 0, tileArray.Length - 1);
                terrainTilemap.SetTile(new Vector3Int(x - mapWidth / 2, y - mapHeight / 2, 0), tileArray[tileIndex]);
            }
        }
    }
}
