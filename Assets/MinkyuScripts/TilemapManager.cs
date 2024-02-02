using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;


[System.Serializable]
public class TileInfo
{
    public TileBase tile;
    public float HP;

    // 다른 속성들을 필요에 따라 추가
}
public class TilemapManager : MonoBehaviour
{
    public static TilemapManager instance;

    public Tilemap tilemap;
    public Tilemap ceilingTile;
    public Dictionary<Vector3Int, TileInfo> wallDictionary = new();

    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        // Bounds는 구조체
        // 해당 Tile의 크기만큼 싹 for문을 돌린다. tile이 있으면 Dictionary에 추가. 각각 TileInfo의 값을 받는다.
        BoundsInt bounds = tilemap.cellBounds;
        for (int x = bounds.x; x < bounds.x + bounds.size.x; x++)
        {
            for (int y = bounds.y; y < bounds.y + bounds.size.y; y++)
            {
                Vector3Int cellPosition = new(x, y, 0);
                TileBase tile = tilemap.GetTile(cellPosition);                
                if (tile != null)
                {
                    float tileHP = GetTileHp(cellPosition);
                    wallDictionary[cellPosition] = new TileInfo
                    {
                        tile = tile,
                        HP = tileHP
                    };
                }
            }
        }
    }


    private float GetTileHp(Vector3Int position)
    {
        int tileHP = 150;
        int Level2Point = 37;
        if (Mathf.Abs(position.x) > Level2Point || Mathf.Abs(position.y) > Level2Point)
        {
            tileHP = 150;
        }
        else
            tileHP = 100;

        return tileHP;
        
    }
    /*private void OnDrawGizmos()
    {
        BoundsInt bounds = tilemap.cellBounds;
        Gizmos.color = Color.green;
        for (int x = bounds.x; x < bounds.x + bounds.size.x; x++)
        {
            for (int y = bounds.y; y < bounds.y + bounds.size.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPosition);
                Gizmos.DrawWireCube(cellCenter, tilemap.cellSize);
            }
        }
    }*/
}
