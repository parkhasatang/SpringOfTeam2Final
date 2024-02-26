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
    public TileMapControl tileMapControl;

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
                    float tileHP = GetTileHp(cellPosition, tile);
                    float tileMinDamage = GetTileMinDamage(cellPosition, tile);
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


    private float GetTileHp(Vector3Int position, TileBase tile)
    {
        string wallname = tile.name;

        if (wallname == "WallRuleTile")
        {
            return 50f;
        }
        else if (wallname == "Wall2RuleTile")
        {
            return 150f;
        }
        else
        {
            Debug.LogError("으아아아아 타일이 없는곳에 벽의 체력값이 들어갔어요 주인님!");
            return 0f;
        }
    }

    private float GetTileMinDamage(Vector3Int position, TileBase tile)
    {
        string wallname = tile.name;

        if (wallname == "WallRuleTile")
        {
            return 20f;
        }
        else if (wallname == "Wall2RuleTile")
        {
            return 70f;
        }
        else
        {
            Debug.LogError("으아아아아 타일이 없는곳에 채굴피해 최소값이 들어갔어요 주인님!");
            return 0f;
        }
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
