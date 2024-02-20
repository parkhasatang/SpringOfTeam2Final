using UnityEngine;

public class LandmarkPlacer : MonoBehaviour
{
    public GameObject[] forestLandmarks;
    public GameObject[] caveLandmarks;
    public WorldGenerator worldGenerator;
    public float landmarkPlacementThreshold = 0.8f; // 랜드마크 배치를 위한 임계값

    void Start()
    {
        worldGenerator.OnGenerationComplete += PlaceLandmarks;
    }

    void OnDestroy()
    {
        worldGenerator.OnGenerationComplete -= PlaceLandmarks;
    }

    void PlaceLandmarks()
    {
        for (int x = 0; x < worldGenerator.mapWidth; x++)
        {
            for (int y = 0; y < worldGenerator.mapHeight; y++)
            {
                float noiseValue = Mathf.PerlinNoise(x * worldGenerator.noiseScale * 2, y * worldGenerator.noiseScale * 2);
                if (noiseValue > landmarkPlacementThreshold)
                {
                    GameObject[] landmarkArray = worldGenerator.themeMap[x, y] == 0 ? forestLandmarks : caveLandmarks;
                    if (landmarkArray.Length == 0) continue;

                    GameObject landmarkPrefab = landmarkArray[Random.Range(0, landmarkArray.Length)];
                    Vector3 position = new Vector3(x - worldGenerator.mapWidth / 2, 0, y - worldGenerator.mapHeight / 2);
                    Instantiate(landmarkPrefab, position, Quaternion.identity);
                }
            }
        }
    }
}