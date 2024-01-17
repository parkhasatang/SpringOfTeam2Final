using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private CharacterController controller;

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
        Debug.Log("상호 작용이닷");
    }

}
