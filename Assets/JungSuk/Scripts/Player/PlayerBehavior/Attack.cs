using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Attack : MonoBehaviour
{
    private CharacterController controller;
    private CharacterStatHandler statsHandler;
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        statsHandler = GetComponent<CharacterStatHandler>();
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        controller.OnAttackEvent += PlayerAttack;
        controller.OnAttackEvent += PlayerMining;
    }

    private void PlayerAttack()
    {
               
    }

    private void PlayerMining()
    {
        Debug.Log("´ê¾Ò´Ù.");
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector3)mousePosition - gameObject.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.5f, 1 << 6);
        if (hit)
        {
            Debug.Log("´ê¾Ò´Ù2.");
            Vector3Int cellPosition = TilemapManager.instance.tilemap.WorldToCell(hit.point);
            if (cellPosition == null)
            {
                return;
            }
            else
            {
                if (TilemapManager.instance.wallDictionary[cellPosition].HP > 0f)
                {
                    TilemapManager.instance.wallDictionary[cellPosition].HP -= statsHandler.CurrentStats.miningAttack;
                    Debug.Log(TilemapManager.instance.wallDictionary[cellPosition].HP);
                    if (TilemapManager.instance.wallDictionary[cellPosition].HP <= 0f)
                    {
                        TilemapManager.instance.tilemap.SetTile(TilemapManager.instance.tilemap.WorldToCell(cellPosition), null);
                    }
                }
            }
        }
    }
}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
        
    //    Vector3Int mousePos = new Vector3Int(Mathf.FloorToInt(collision.transform.position.x), Mathf.FloorToInt(collision.transform.position.y), 0);
    //    TilemapManager.instance.wallDictionary.TryGetValue(mousePos, out TileInfo tileInfo);
 
    //    if(tileInfo.HP > 0)
    //    {
    //        tileInfo.HP -= statsHandler.CurrentStats.baseStatsSO.miningAttack;

    //        if(tileInfo.HP <= 0)
    //        {
    //            TilemapManager.instance.tilemap.SetTile(TilemapManager.instance.tilemap.WorldToCell(mousePos), null);
    //        }

    //    }            
        
    //}



