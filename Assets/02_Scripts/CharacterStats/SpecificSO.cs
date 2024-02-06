using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BaseStatsData", menuName = "CharacterController/BaseStats", order = 0)]
public class SpecificSO : ScriptableObject
{
    [Header("PlayerInfo")]
    public float maxHunger;
    public float hunger;
    public float decreaseHungerTime;
    public float useCoolTime;

    [Header("MonsterInfo")]
    public float followDistance;
    public float attackRange;
    public float currentHP;
    public float speed;
    public float attackDamage;


}
