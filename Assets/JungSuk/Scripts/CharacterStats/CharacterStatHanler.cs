using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHanler : MonoBehaviour
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
        if(playerBaseStats.playerBaseStatsSO != null)
        {
            baseStatsSO = Instantiate(playerBaseStats.playerBaseStatsSO);
        }

        CurrentStats = new PlayerStats { playerBaseStatsSO = baseStatsSO };
        CurrentStats.statsChangeType = playerBaseStats.statsChangeType;
        CurrentStats.hunger = playerBaseStats.hunger;
        CurrentStats.decreaseHungerTime = playerBaseStats.decreaseHungerTime;
        CurrentStats.useCoolTime= playerBaseStats.useCoolTime;
    }

    
}
