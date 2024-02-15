using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager Instance;
    private GameObject player;
    private CharacterStatHandler playerStatHandler;
    public  Item[] equipItem;
    public EquipSlot[] equipSlots;

    private float playerHp;
    private float playerDefense;
    private float playerSpeed;

    private void Awake()
    {
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

        for (int i = 0; i < equipItem.Length; i++)
        {
            if (equipItem[i] != null)
            {
                playerStatHandler.CurrentStats.maxHP += equipItem[i].HP;
                playerStatHandler.CurrentStats.defense += equipItem[i].Defense;
                playerStatHandler.CurrentStats.speed += equipItem[i].Speed;
            }
            else
                continue;
        }

        Debug.Log(playerStatHandler.CurrentStats.maxHP);
        Debug.Log(playerStatHandler.CurrentStats.defense);
        Debug.Log(playerStatHandler.CurrentStats.speed);
    }
}
