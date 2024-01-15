using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BaseStatsData", menuName = "CharacterController/BaseStats", order = 0)]
public class BaseStats : ScriptableObject
{
    [Header("BaseStats")]
    public string name;
    public float speed;
    public float maxHP;


    [Header ("Battle Info")]
    public float attackDamage;
    public float defense;
    public float miningAttack;
    public float attackDelay;
}
