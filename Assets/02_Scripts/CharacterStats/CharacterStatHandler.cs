using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipedItem
{
    void EquipItemForChangeStats(int itemIndex);
    void UnEquipItemForChangeStats(int itemIndex);
}

public interface IUsePotion
{
    void UsePotionForChangeStats(int ItemNumver);
}
public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] private CharacterStats characterBaseStats;
    [SerializeField] private MonsterStats monsterStats;

    public CharacterStats CurrentStats { get; private set; }
    public MonsterStats CurrentMonsterStats { get; private set; }

    public CharacterStats ChangeStats1;
    public CharacterStats ChangeStats2;
    private void Awake()
    {
        UpdateCharacterStats();
    }

    public void UpdateCharacterStats()
    {
        SpecificSO specificSO = null;
        if (characterBaseStats.specificSO != null)
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
        CurrentStats.HP = characterBaseStats.HP;
        CurrentStats.attackDamage = characterBaseStats.attackDamage;
        CurrentStats.defense = characterBaseStats.defense;
        CurrentStats.miningAttack = characterBaseStats.miningAttack;
        CurrentStats.attackDelay = characterBaseStats.attackDelay;
       

        if (monsterStats != null)
        {
            CurrentMonsterStats = new MonsterStats
            {
                speed = monsterStats.speed,
                maxHP = monsterStats.maxHP,
                HP = monsterStats.HP,
                attackDamage = monsterStats.attackDamage,
                defense = monsterStats.defense,
                attackDelay = monsterStats.attackDelay,
                followDistance = monsterStats.followDistance,
                attackRange = monsterStats.attackRange
            };
        }
    }
}
