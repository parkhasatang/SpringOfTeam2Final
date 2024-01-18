using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] private PlayerStats playerBaseStats;

    public PlayerStats CurrentStats { get; private set; }
   

    private void Awake()
    {
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        BaseStatsSO baseStatsSO = null;
        if(playerBaseStats.baseStatsSO != null)
        {
            baseStatsSO = Instantiate(playerBaseStats.baseStatsSO);
        }

        CurrentStats = new PlayerStats { baseStatsSO = baseStatsSO };
        CurrentStats.statsChangeType = playerBaseStats.statsChangeType;
        CurrentStats.hunger = playerBaseStats.hunger;
        CurrentStats.decreaseHungerTime = playerBaseStats.decreaseHungerTime;
        CurrentStats.useCoolTime= playerBaseStats.useCoolTime;
    }

    
}
