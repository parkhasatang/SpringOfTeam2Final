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
[Serializable]
public class CharacterStats 
{
    public StatsChangeType statsChangeType;

    public string name;
    public float speed;
    public float maxHP;

    [Header("Battle Info")]
    public float attackDamage;
    public float defense;
    public float miningAttack;
    public float attackDelay;

    public BaseStatsSO baseStatsSO;
}
