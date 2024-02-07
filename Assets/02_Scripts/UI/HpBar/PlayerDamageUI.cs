using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDamageUI : MonoBehaviour
{    
    public TextMeshPro damageTxt;
    public float damage;

   
    // Start is called before the first frame update
    void Start()
    {
        damageTxt.text = damage.ToString();
        Invoke ("DestroyObject", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
