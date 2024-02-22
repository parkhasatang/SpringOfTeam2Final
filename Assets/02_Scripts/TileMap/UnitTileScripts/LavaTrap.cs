using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class LavaTrap : MonoBehaviour
{
    private float damage = 10f;
    private HealthSystem healthSystem = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Player"))
        {
           
            healthSystem = collision.GetComponent<HealthSystem>();
            healthSystem.ChangeHealth(-damage);
        }
        else if (collision.CompareTag("Monster"))
        {
            healthSystem = collision.GetComponent<HealthSystem>();
            healthSystem.ChangeMHealth(-damage);
        }
        else
        {

        }
    }   
}
