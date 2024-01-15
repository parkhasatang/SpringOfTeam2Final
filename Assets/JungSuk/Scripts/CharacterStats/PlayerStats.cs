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
public class PlayerStats 
{
    public StatsChangeType statsChangeType;
    public float hunger;
    public float decreaseHungerTime;
    public float useCoolTime;

    public BaseStatsSO playerBaseStatsSO;
}
