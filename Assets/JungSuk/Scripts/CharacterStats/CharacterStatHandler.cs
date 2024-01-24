using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] private CharacterStats characterBaseStats;

    public CharacterStats CurrentStats { get; private set; }
   

    private void Awake()
    {
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        SpecificSO specificSO = null;
        if(characterBaseStats.specificSO != null)
        {
            specificSO = Instantiate(characterBaseStats.specificSO);
        }
        // 플레이어
        CurrentStats = new CharacterStats { specificSO = specificSO };
        CurrentStats.statsChangeType = characterBaseStats.statsChangeType;
        CurrentStats.objectType = characterBaseStats.objectType;        
        CurrentStats.name = characterBaseStats.name;
        CurrentStats.speed = characterBaseStats.speed;
        CurrentStats.maxHP = characterBaseStats.maxHP;
        CurrentStats.HP= characterBaseStats.HP;
        CurrentStats.attackDamage = characterBaseStats.attackDamage;
        CurrentStats.defense = characterBaseStats.defense;
        CurrentStats.miningAttack = characterBaseStats.miningAttack;
        CurrentStats.attackDelay = characterBaseStats.attackDelay;

        
    }

    
}
