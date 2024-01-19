using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BaseStatsData", menuName = "CharacterController/BaseStats", order = 0)]
public class BaseStatsSO : ScriptableObject
{
    [Header ("PlayerInfo")]
    public float hunger;
    public float decreaseHungerTime;
    public float useCoolTime;

    [Header("MonsterInfo")]
    public float followDistance;
    public float attackRange;

    
}
