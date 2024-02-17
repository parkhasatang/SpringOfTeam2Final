using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnDamageUI : MonoBehaviour
{    
    public GameObject damageTxtPrefab;                 
    private List<GameObject> damageTxtPools;

    private void Awake()
    {
        damageTxtPools = new List<GameObject>();
        for(int i= 0; i<3; i++)
        {
            GameObject damageTxt = Instantiate(damageTxtPrefab);            
            damageTxt.SetActive(false);
            damageTxtPools.Add(damageTxt);

        }
       
    }
    // Start is called before the first frame update
    void Start()
    {        
        
    }

    public void SpawndamageTxt(Vector3 MonsterPos, float change)
    {
        GameObject _damageTxt = null;
        foreach (GameObject Txt in damageTxtPools)
        {
            if (!Txt.activeSelf)
            {
                _damageTxt = Txt;
                _damageTxt.GetComponent<PlayerDamageUI>().damage = change;
                _damageTxt.transform.position = MonsterPos + new Vector3(0,1f,0);                
                Txt.SetActive(true);
                Debug.Log("나타나기");
                break;                
            }
        }

        if (!_damageTxt)
        {
            _damageTxt = Instantiate(damageTxtPrefab);
            _damageTxt.GetComponent<PlayerDamageUI>().damage = change;
            _damageTxt.transform.position = MonsterPos + new Vector3(0,1f,0);
            _damageTxt.SetActive(true);            
            Debug.Log("새로 만들기");
        }
    }
}
