using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemStatsData", menuName = "CharacterController/ItemStats", order = 2)]
public class IteamStats : ScriptableObject
{
    public float HP;
    public float Damage;
    public float hunger;
    public float Speed;
    public float defense;
    public float attackRange;
    public float attackDelay;
}
