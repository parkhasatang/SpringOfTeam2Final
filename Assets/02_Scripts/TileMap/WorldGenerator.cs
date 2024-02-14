using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class WorldGenerator : MonoBehaviour
{
    public Tilemap terrainTilemap;
    public Tile[] terrainTiles;

    public int mapWidth = 100;
    public int mapHeight = 100;
    public float noiseScale = 0.3f;
    public event Action OnGenerationComplete;

    void Start()
    {
        GenerateWorld();

        OnGenerationComplete?.Invoke();
    }

    void GenerateWorld()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float noiseValue = Mathf.PerlinNoise(x * noiseScale, y * noiseScale);
                int tileIndex = Mathf.FloorToInt(noiseValue * terrainTiles.Length);
                tileIndex = Mathf.Clamp(tileIndex, 0, terrainTiles.Length - 1); // 배열 범위의 초과를 방지함
                terrainTilemap.SetTile(new Vector3Int(x - mapWidth / 2, y - mapHeight / 2, 0), terrainTiles[tileIndex]);
            }
        }
    }
}