using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAim : MonoBehaviour
{
    [SerializeField] private SpriteRenderer weaponRenderer;
    [SerializeField] private Transform armPivot;
    [SerializeField] private SpriteRenderer characterRenderer;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 value)
    {
        RotateCharacter(value);
    }

    private void RotateCharacter(Vector2 value)
    {
        
    }

  
}
