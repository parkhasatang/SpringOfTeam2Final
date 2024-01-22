using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsChangeType
{
    Add,
    Multiple,
    Override,
}

public enum ObjectType
{
    Player,
    Monster,
    CanectWeapon,
    RangeWeapon,
    MineWeapon,
    HPPotion,
    HungerPotion,
    InstalltionItem,
    InteractionItem,
}
[Serializable]
public class CharacterStats 
{
    public StatsChangeType statsChangeType;
    public ObjectType objectType;

    public string name;
    public float speed;
    public float maxHP;

    [Header("Battle Info")]
    public float attackDamage;
    public float defense;
    public float miningAttack;
    public float attackDelay;

    public SpecificSO specificSO;
}
