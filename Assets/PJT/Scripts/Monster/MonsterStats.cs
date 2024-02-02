using System;
using UnityEngine;

[Serializable]
public class MonsterStats
{
    public string name;
    public float speed;
    public float maxHP;
    public float HP;

    [Header("Monster Specific Stats")]
    public float followDistance;
    public float attackRange;
    public float attackDamage;
    public float defense;
    public float attackDelay;

    public SpecificSO specificSO;
}