using UnityEngine;
using System;
using System.Collections.Generic;

public class LandmarkPlacer : MonoBehaviour
{
    public WorldGenerator worldGenerator;
    public GameObject forestMaze;
    public GameObject seaTemple;
    public GameObject dungeonBossRoom;
    public List<Vector3> landmarkPositions = new List<Vector3>();
    public event Action OnLandmarksPlaced;

    private void Awake()
    {
        if (worldGenerator != null)
        {
            worldGenerator.OnGenerationComplete += PlaceLandmarks;
        }
    }

    private void OnDestroy()
    {
        if (worldGenerator != null)
        {
            worldGenerator.OnGenerationComplete -= PlaceLandmarks;
        }
    }

    private void PlaceLandmarks()
    {
        PlaceLandmark(forestMaze, 0);
        PlaceLandmark(seaTemple, 1);
        PlaceLandmark(dungeonBossRoom, 2);
        Debug.Log("랜드마크 완료");
        OnLandmarksPlaced?.Invoke();
    }

    private void PlaceLandmark(GameObject landmarkPrefab, int themeIndex)
    {
        Vector3 position = CalculateRandomPositionForTheme(themeIndex);
        if (position != Vector3.zero)
        {
            position.z = 0f;
            Instantiate(landmarkPrefab, position, Quaternion.identity);
            int x = (int)(position.x + worldGenerator.mapWidth / 2);
            int y = (int)(position.y + worldGenerator.mapHeight / 2);

            worldGenerator.landmarkPlaced[x, y] = true;
        }
    }

    private Vector3 CalculateRandomPositionForTheme(int themeIndex)
    {
        var validPositions = new System.Collections.Generic.List<Vector3>();

        for (int x = 0; x < worldGenerator.mapWidth; x++)
        {
            for (int y = 0; y < worldGenerator.mapHeight; y++)
            {

                if (worldGenerator.themeMap[x, y] == themeIndex && !worldGenerator.landmarkPlaced[x, y])
                {
                    validPositions.Add(new Vector3(x - worldGenerator.mapWidth / 2, 0, y - worldGenerator.mapHeight / 2));
                }
            }
        }

        if (validPositions.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, validPositions.Count);
            return validPositions[randomIndex];
        }

        return Vector3.zero;
    }
}