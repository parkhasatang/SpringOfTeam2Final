using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private TileMapControl tileMapControl;
    private void Update()
    {
        Vector2 mousPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousPosition, transform.position);
        if (Input.GetMouseButtonDown(0) && distance < 10)
        {
            tileMapControl.CreateTile(Mathf.FloorToInt(mousPosition.x),Mathf.FloorToInt(mousPosition.y));
        }
    }
}
