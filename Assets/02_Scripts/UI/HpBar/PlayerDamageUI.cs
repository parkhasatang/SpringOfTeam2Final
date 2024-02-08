using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Build.Content;

public class PlayerDamageUI : MonoBehaviour
{    
    public TextMeshPro damageTxt;
    public float damage;

   
    // Start is called before the first frame update
    void Start()
    {               
    }

    // Update is called once per frame
    void Update()
    {
        damageTxt.text = damage.ToString();
        Invoke("DestroyObject", 1.5f);
    }

    private void DestroyObject()
    {
        gameObject.SetActive (false);
    }
}
