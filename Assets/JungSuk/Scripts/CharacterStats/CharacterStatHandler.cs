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
        BaseStatsSO baseStatsSO = null;
        if(characterBaseStats.baseStatsSO != null)
        {
            baseStatsSO = Instantiate(characterBaseStats.baseStatsSO);
        }
        // 플레이어
        CurrentStats = new CharacterStats { baseStatsSO = baseStatsSO };
        CurrentStats.statsChangeType = characterBaseStats.statsChangeType;
        CurrentStats.name = characterBaseStats.name;
        CurrentStats.maxHP = characterBaseStats.maxHP;
        CurrentStats.attackDamage = characterBaseStats.attackDamage;
        CurrentStats.defense = characterBaseStats.defense;
        CurrentStats.miningAttack = characterBaseStats.miningAttack;
        CurrentStats.attackDelay = characterBaseStats.attackDelay;

    }

    
}
