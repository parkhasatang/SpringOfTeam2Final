using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapControl : MonoBehaviour
{
    public TileBase newTile;

    public void CreateTile(int x, int y)
    {
        Vector3 cellPosition = new Vector3(x, y, 0);
        TilemapManager.instance.tilemap.SetTile(TilemapManager.instance.tilemap.WorldToCell(cellPosition), newTile);
    }

    public void DestroyTile(int x, int y)
    {
        Vector3 cellPosition = new Vector3(x, y, 0);
        TilemapManager.instance.tilemap.SetTile(TilemapManager.instance.tilemap.WorldToCell(cellPosition), null);
    }
}
