using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager Instance;
    private GameObject player;
    private CharacterStatHandler playerStatHandler;   
    public EquipSlot[] equipSlots;

    private float playerHp;
    private float playerDefense;
    private float playerSpeed;

    private void Awake()
    {
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatHandler = player.GetComponent<CharacterStatHandler>();       
    }
    // Start is called before the first frame update
    void Start()
    {                
        playerHp = playerStatHandler.CurrentStats.HP;
        playerDefense = playerStatHandler.CurrentStats.defense;
        playerSpeed = playerStatHandler.CurrentStats.speed;
    }
    public void UpdatePlayerStat()
    {
        
        playerStatHandler.CurrentStats.maxHP = playerHp;
        playerStatHandler.CurrentStats.defense = playerDefense;
        playerStatHandler.CurrentStats.speed = playerSpeed;     
        
        for(int i = 0; i < equipSlots.Length; i++)
        {
            if (equipSlots[i].equipItemData != null)
            {
                playerStatHandler.CurrentStats.maxHP += equipSlots[i].equipItemData.HP;
                playerStatHandler.CurrentStats.defense += equipSlots[i].equipItemData.Defense;
                playerStatHandler.CurrentStats.speed += equipSlots[i].equipItemData.Speed;
            }
            else
                continue;
        }              
    }
}
