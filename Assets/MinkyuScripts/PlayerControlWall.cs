using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlWall : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private TileMapControl tileMapControl;

    private void Update()
    {
        Vector2 mousPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousPosition, transform.position);
        if (Input.GetMouseButtonDown(0) && distance < 10)
        {
            if (!TilemapManager.instance.wallDictionary.ContainsKey(new Vector3Int(Mathf.FloorToInt(mousPosition.x), Mathf.FloorToInt(mousPosition.y), 0)))
            {
                tileMapControl.CreateTile(Mathf.FloorToInt(mousPosition.x), Mathf.FloorToInt(mousPosition.y));
                // 새롭게 생성해준 Tile을 Dictionary에 추가.
                TilemapManager.instance.wallDictionary[new Vector3Int(Mathf.FloorToInt(mousPosition.x), Mathf.FloorToInt(mousPosition.y), 0)] = new TileInfo
                {
                    tile = tileMapControl.newTile,
                    HP = 100f
                };
                Debug.Log("딕셔너리에 추가");
            }
        }
    }
}
