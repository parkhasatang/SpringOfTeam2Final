using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private CharacterController controller;
    public Collider2D rangebox;
    private Dictionary<string, GameObject> interactiveUIs = new Dictionary<string, GameObject>();
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        
    }
    // Start is called before the first frame update
    //private void Start()
    //{
    //    controller.OnInteractEvent += PlayerInteract;
    //}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CancelUI();
        }
    }

    //private void PlayerInteract()
    //{
    //    OnTriggerStay2D(rangebox);
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {       
         if (controller.IsInteracting && collision.CompareTag("InteractionObject"))
        {
            string Name = collision.gameObject.name;

            if(interactiveUIs.ContainsKey(Name))
            {
                interactiveUIs[Name].SetActive(true);
                controller.CanControllCharacter = false;
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
                        controller.CanControllCharacter = false;
                        interactiveUIs.Add(Name, instantiatedUI);
                    }
                }
                
            }                               
        }
    }
   
    public void CancelUI()
    {
       foreach(var ui in  interactiveUIs)
        {
            ui.Value.SetActive(false);
        }
        controller.CanControllCharacter = true;
    }
}
