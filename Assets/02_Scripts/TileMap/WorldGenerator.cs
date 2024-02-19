using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class WorldGenerator : MonoBehaviour
{
    public Tilemap terrainTilemap;
    public Tile[] forestTiles;
    public Tile[] caveTiles;
    public int[,] themeMap;

    public int mapWidth = 100;
    public int mapHeight = 100;
    public float noiseScale = 0.3f;
    public float themeScale = 0.05f; // 테마 스케일 조정
    public float forestThemeThreshold = 0.4f; // 숲 테마 임계값 조정


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
                float forestNoise = Mathf.PerlinNoise((x + 10000) * themeScale, (y + 10000) * themeScale);
                float caveNoise = Mathf.PerlinNoise((x + 30000) * themeScale, (y + 30000) * themeScale);

                // 숲과 동굴 지역의 분포를 결정하는 병합 로직
                if (forestNoise > caveNoise && forestNoise > 0.5)
                {
                    themeMap[x, y] = 0; // 숲 지역으로 설정
                }
                else if (caveNoise > forestNoise && caveNoise > 0.5)
                {
                    themeMap[x, y] = 1; // 동굴 지역으로 설정
                }
                // 추가 조건 및 테마 설정 가능

                // 테마에 따라 타일 배치
                Tile tileToPlace = themeMap[x, y] == 0 ? forestTiles[UnityEngine.Random.Range(0, forestTiles.Length)] :
                    themeMap[x, y] == 1 ? caveTiles[UnityEngine.Random.Range(0, caveTiles.Length)] : null;
                if (tileToPlace != null)
                {
                    terrainTilemap.SetTile(new Vector3Int(x - mapWidth / 2, y - mapHeight / 2, 0), tileToPlace);
                }
            }
        }
    }
}