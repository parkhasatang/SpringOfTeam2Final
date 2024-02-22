using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRequest : MonoBehaviour
{
    private Quest quest;

    private void Awake()
    {
        quest = GetComponentInParent<Quest>();
    }

    private void OnEnable()
    {
        quest.CheckRequst();
    }
}
