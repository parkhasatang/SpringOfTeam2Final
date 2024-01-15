using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHanler : MonoBehaviour
{
    [SerializeField] private PlayerStats playerBaseStats;

    public PlayerStats CurrentStats { get; private set; }
    public List<PlayerStats> statsModifiers = new List<PlayerStats>();

    private void Awake()
    {
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        BaseStatsSO baseStats = null;
        if(playerBaseStats.playerBaseStatsSO != null)
        {
            baseStats = Instantiate(playerBaseStats.playerBaseStatsSO);
        }

        CurrentStats = new PlayerStats { playerBaseStatsSO = baseStats };
    }
}
