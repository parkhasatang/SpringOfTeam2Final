using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStatsData", menuName = "CharacterController/MonsterStats", order = 1)]
public class MonsterStatsSO : ScriptableObject
{
    [Header("Monster Specific Stats")]
    public float currentHP;
    public float followDistance;
    public float attackRange;

}