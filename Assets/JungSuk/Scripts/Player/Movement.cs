using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController _controller;
    private Vector2 _moveValue = Vector2.zero; // 매게변수 값 넣어줘서 이 클래스 내에서 사용할 벡터값
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();       
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _controller.OnMoveEvent += Move;
    }

    private void Move(Vector2 value)
    {
        _moveValue = value;
    }

    private void ApplyMovement(Vector2 value)
    {
        value = value * 5; // 나중에 캐릭터 스피드 변수로 교체 예정
        _rigidbody.velocity = value;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        ApplyMovement(_moveValue);
    }
}
