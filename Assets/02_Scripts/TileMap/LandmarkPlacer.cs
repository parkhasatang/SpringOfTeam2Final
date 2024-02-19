using UnityEngine;

public class LandmarkPlacer : MonoBehaviour
{
    public GameObject[] forestLandmarks; // 숲 지역에 배치할 랜드마크 배열
    public GameObject[] caveLandmarks; // 동굴 지역에 배치할 랜드마크 배열
    public WorldGenerator worldGenerator;
    public float landmarkThreshold = 0.8f;

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
                if (noiseValue > landmarkThreshold)
                {
                    Vector3 position = new Vector3(x - worldGenerator.mapWidth / 2, 0, y - worldGenerator.mapHeight / 2);
                    GameObject[] landmarkArray = null;

                    if (worldGenerator.themeMap[x, y] == 0) // 숲 지역
                    {
                        landmarkArray = forestLandmarks;
                    }
                    else if (worldGenerator.themeMap[x, y] == 1) // 동굴 지역
                    {
                        landmarkArray = caveLandmarks;
                    }

                    if (landmarkArray != null && landmarkArray.Length > 0)
                    {
                        GameObject landmarkPrefab = landmarkArray[Random.Range(0, landmarkArray.Length)];
                        Instantiate(landmarkPrefab, position, Quaternion.identity);
                    }
                }
            }
        }
    }
}
