using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    private void PlayerAttack()
    {
               
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



