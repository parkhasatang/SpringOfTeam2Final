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
            GameObject landmarkInstance = Instantiate(landmarkPrefab, position, Quaternion.identity);
            Bounds totalBounds = CalculateTotalBounds(landmarkInstance);

            int minX = Mathf.FloorToInt(totalBounds.min.x + worldGenerator.mapWidth / 2);
            int maxX = Mathf.CeilToInt(totalBounds.max.x + worldGenerator.mapWidth / 2);
            int minY = Mathf.FloorToInt(totalBounds.min.y + worldGenerator.mapHeight / 2);
            int maxY = Mathf.CeilToInt(totalBounds.max.y + worldGenerator.mapHeight / 2);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    if (x >= 0 && x < worldGenerator.mapWidth && y >= 0 && y < worldGenerator.mapHeight)
                    {
                        worldGenerator.landmarkPlaced[x, y] = true;
                    }
                }
            }
        }
    }


    private Vector3 CalculateRandomPositionForTheme(int themeIndex)
    {
        List<Vector3> validPositions = new List<Vector3>();
        float minDistanceFromCenter = 5f;

        Vector2 centerTilePosition = new Vector2(worldGenerator.center.x, worldGenerator.center.y);

        for (int x = 0; x < worldGenerator.mapWidth; x++)
        {
            for (int y = 0; y < worldGenerator.mapHeight; y++)
            {
                Vector3 position = new Vector3(x - worldGenerator.mapWidth / 2, y - worldGenerator.mapHeight / 2, 0);
                Vector2 tilePosition = new Vector2(x, y);

                float distanceFromCenter = Vector2.Distance(tilePosition, centerTilePosition);

                if (worldGenerator.themeMap[x, y] == themeIndex && !worldGenerator.landmarkPlaced[x, y] && distanceFromCenter > minDistanceFromCenter + worldGenerator.centralCircleRadius)
                {
                    validPositions.Add(position);
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
    private Bounds CalculateTotalBounds(GameObject landmarkInstance)
    {
        Renderer[] renderers = landmarkInstance.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return new Bounds(Vector3.zero, Vector3.zero);

        Bounds totalBounds = renderers[0].bounds;
        foreach (Renderer renderer in renderers)
        {
            totalBounds.Encapsulate(renderer.bounds);
        }
        return totalBounds;
    }
}