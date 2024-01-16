using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private CharacterController controller;
    public Collider2D rangebox;
    private bool InteractionUIOn = false;
    private Dictionary<string, GameObject> interactiveUIs = new Dictionary<string, GameObject>();
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
         if (controller.IsInteracting && collision.CompareTag("InteractionObject") && InteractionUIOn == false)
        {
            InteractionUIOn=true;
            string Name = collision.gameObject.name;

            if(interactiveUIs.ContainsKey(Name))
            {
                interactiveUIs[Name].SetActive(true);
            }
            else
            {
                GameObject UI = Resources.Load<GameObject>($"UI/{Name}");
                
                if(UI != null)
                {
                    GameObject instantiatedUI = Instantiate(UI);

                    if(instantiatedUI != null)
                    {
                        instantiatedUI.SetActive(true);

                        interactiveUIs.Add(Name, instantiatedUI);
                    }
                }
                
            }                               
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelnteractiveUI(collision.gameObject.name);
    }
    public void CancelnteractiveUI(string name)
    {
        InteractionUIOn = false;
        if(interactiveUIs.ContainsKey(name))
        {
            interactiveUIs[name].SetActive(false);
        }
    }
}
