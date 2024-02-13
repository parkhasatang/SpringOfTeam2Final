using UnityEngine;
using UnityEngine.Tilemaps;

public class LandmarkPlacer : MonoBehaviour
{
    public Tilemap landmarkTilemap;
    public Tile landmarkTile;

    public WorldGenerator worldGenerator;
    public float landmarkThreshold = 0.8f;

    void Start()
    {
        PlaceLandmarks();
    }

    void PlaceLandmarks()
    {
        for (int x = 0; x < worldGenerator.mapWidth; x++)
        {
            for (int y = 0; y < worldGenerator.mapHeight; y++)
            {
                // 랜드마크 배치는 지형 생성 후에 이루어져야 하므로 스케일을 조정
                float noiseValue = Mathf.PerlinNoise(x * worldGenerator.noiseScale * 2, y * worldGenerator.noiseScale * 2);
                if (noiseValue > landmarkThreshold)
                {
                    landmarkTilemap.SetTile(new Vector3Int(x - worldGenerator.mapWidth / 2, y - worldGenerator.mapHeight / 2, 0), landmarkTile);
                }
            }
        }
    }
}