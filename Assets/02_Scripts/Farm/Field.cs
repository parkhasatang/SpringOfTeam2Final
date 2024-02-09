using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject seed;

    [SerializeField]internal bool isWatering;
    private bool isSeed;
    private Item seedData;

    private void OnEnable()
    {
        isWatering = false;
        isSeed = false;
        seed.SetActive(false);
        seedData = null;
    }
}
