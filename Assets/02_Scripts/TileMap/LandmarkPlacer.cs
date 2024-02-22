using UnityEngine;

public class LandmarkPlacer : MonoBehaviour
{
    public WorldGenerator worldGenerator;
    public GameObject forestMaze;
    public GameObject seaTemple;
    public GameObject dungeonBossRoom;
    public GameObject graveyard;

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
        PlaceLandmark(forestMaze, "Forest");
        PlaceLandmark(seaTemple, "Sea");
        PlaceLandmark(dungeonBossRoom, "Dungeon");
        PlaceLandmark(graveyard, "Graveyard");
    }

    private void PlaceLandmark(GameObject landmarkPrefab, string theme)
    {
        Vector3 position = CalculatePositionForLandmark(theme);
        if (position != Vector3.zero)
        {
            Instantiate(landmarkPrefab, position, Quaternion.identity);
        }
    }

    private Vector3 CalculatePositionForLandmark(string theme)
    {
        int themeIndex = GetThemeIndex(theme);
        if (themeIndex == -1)
        {
            Debug.LogError("Unknown theme: " + theme);
            return Vector3.zero;
        }

        for (int x = 0; x < worldGenerator.mapWidth; x++)
        {
            for (int y = 0; y < worldGenerator.mapHeight; y++)
            {
                if (worldGenerator.themeMap[x, y] == themeIndex)
                {
                    return new Vector3(x - worldGenerator.mapWidth / 2, 0, y - worldGenerator.mapHeight / 2);
                }
            }
        }
        return Vector3.zero;
    }

    private int GetThemeIndex(string theme)
    {
        switch (theme)
        {
            case "Forest":
                return 0;
            case "Sea":
                return 1;
            case "Dungeon":
                return 2;
            case "Graveyard":
                return 3;
            default:
                return -1;
        }
    }
}
