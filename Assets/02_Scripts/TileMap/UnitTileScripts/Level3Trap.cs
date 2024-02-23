using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Trap : MonoBehaviour
{
    public Transform reSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = reSpawnPoint.position;
        }
    }
}
