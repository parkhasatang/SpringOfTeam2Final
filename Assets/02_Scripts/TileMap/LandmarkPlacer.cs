using UnityEngine;

public class LandmarkPlacer : MonoBehaviour
{
    public WorldGenerator worldGenerator;
    public GameObject forestMaze;
    public GameObject seaTemple;
    public GameObject dungeonBossRoom;

    private void Start()
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
    }

    private void PlaceLandmark(GameObject landmarkPrefab, int themeIndex)
    {
        Vector3 position = CalculateRandomPositionForTheme(themeIndex);
        if (position != Vector3.zero)
        {
            Instantiate(landmarkPrefab, position, Quaternion.identity);
        }
    }

    private Vector3 CalculateRandomPositionForTheme(int themeIndex)
    {
        var validPositions = new System.Collections.Generic.List<Vector3>();

        for (int x = 0; x < worldGenerator.mapWidth; x++)
        {
            for (int y = 0; y < worldGenerator.mapHeight; y++)
            {
                if (worldGenerator.themeMap[x, y] == themeIndex)
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