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
    public float minMiningAttack;

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
                    float tileMinDamage = GetTileMinDamage(cellPosition);
                    wallDictionary[cellPosition] = new TileInfo
                    {
                        tile = tile,
                        HP = tileHP,
                        minMiningAttack = tileMinDamage
                    };
                }
            }
        }
    }


    private float GetTileHp(Vector3Int position)
    {
        int tileHP = 150;
        int Level2Point = 70;
        if (Mathf.Abs(position.x) > Level2Point || Mathf.Abs(position.y) > Level2Point)
        {
            tileHP = 150;
        }
        else
            tileHP = 100;

        return tileHP;
        
    }

    private float GetTileMinDamage(Vector3Int position)
    {
        int minDamage = 25;
        int Level2Point = 70;
        if (Mathf.Abs(position.x) > Level2Point || Mathf.Abs(position.y) > Level2Point)
        {
            minDamage = 25;
        }
        else
            minDamage = 0;
        return minDamage;
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
