using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private CharacterController controller;
    public Collider2D player;
    private bool deactiveTabKey;
    /*private Dictionary<string, GameObject> interactiveUIs = new Dictionary<string, GameObject>();*/

    public GameObject inventoryObject;
    public GameObject MakeUI;
    public GameObject CookUI;
    public GameObject equipUI;
    public GameObject questUI;
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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!deactiveTabKey)
            {
                ToggleInventory();
            }
        }
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {       
        if (controller.IsInteracting && collision.CompareTag("InteractionObject"))
        {
            string ObjectName = collision.gameObject.name;
            deactiveTabKey = true;

            if (ObjectName == "Make")
            {
                MakeUI.SetActive(true);
                inventoryObject.SetActive(true);
                controller.CanControllCharacter = false;
            }
            else if (ObjectName == "Cook")
            {
                CookUI.SetActive(true);
                inventoryObject.SetActive(true);
                controller.CanControllCharacter = false;
            }
            else if(ObjectName== "Quest")
            {
                questUI.SetActive(true);
                controller.CanControllCharacter = false;
            }

            else if (ObjectName == "TreasureBox")
            {
                TreasureBox box = collision.GetComponent<TreasureBox>();
                box.OpenTreasureBox();
            }
            /*string Name = collision.gameObject.name;

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
                
            }  */
        }
    }
   
    public void CancelUI()
    {
        if (MakeUI.activeSelf || CookUI.activeSelf)
        {
            questUI.SetActive(false);
            MakeUI.SetActive(false);
            CookUI.SetActive(false);
            inventoryObject.SetActive(false);
            controller.CanControllCharacter = true;
            deactiveTabKey = false;
        }
       /*foreach(var ui in  interactiveUIs)
        {
            ui.Value.SetActive(false);
        }
        controller.CanControllCharacter = true;*/
    }

    private void ToggleInventory()
    {
        // 인벤토리 오브젝트를 켜거나 끔
        inventoryObject.SetActive(!inventoryObject.activeSelf);
        equipUI.SetActive(!equipUI.activeSelf);
        if (inventoryObject.activeSelf)
        {
            controller.CanControllCharacter = false;
        }
        else
        {
            controller.CanControllCharacter = true;
        }
    }
}
