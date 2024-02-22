using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TempleWater : MonoBehaviour
{
    private float damage = 3f;
    private HealthSystem healthSystem = null;    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                healthSystem = collision.GetComponent<HealthSystem>();                                
                healthSystem.ChangeHealth(-damage);
            }
        }         
    }
}
