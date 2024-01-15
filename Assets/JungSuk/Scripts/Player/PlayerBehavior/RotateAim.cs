using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAim : MonoBehaviour
{
    [SerializeField] private SpriteRenderer weaponRenderer;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform playertransfrom;

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
        float angle = Mathf.Atan2(value.y, value.x)*Mathf.Rad2Deg;
        weaponPivot.rotation = Quaternion.Euler(playertransfrom.position.x, playertransfrom.position.y, angle);
        if( -45f< angle && angle < 45f)
        {
            characterRenderer.color = Color.white;
        }

        if(45f<= angle && angle < 135f)
        {
            characterRenderer.color = Color.yellow;
        }

        if(135f <= angle && angle <= 180f || -180f < angle && angle < -135f)
        {
            characterRenderer.color = Color.black;
        }

        if(-135f < angle && angle <= -45f)
        {
            characterRenderer.color = Color.blue;
        }
    }

  
}
