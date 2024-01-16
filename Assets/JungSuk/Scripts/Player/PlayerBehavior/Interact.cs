using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private CharacterController controller;
    public Collider2D rangebox;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        
    }
    // Start is called before the first frame update
    private void Start()
    {
        controller.OnInteractEvent += PlayerInteract;
    }

    private void PlayerInteract()
    {
        OnTriggerStay2D(rangebox);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {       
         if (controller.IsInteracting && collision.CompareTag("InteractionObject"))
        {
            Debug.Log("Ãæµ¹Áß");
            string Name = collision.gameObject.name;
            GameObject UI = Resources.Load<GameObject>($"UI/{Name}");
            Instantiate(UI);
            
        }
    }
}
