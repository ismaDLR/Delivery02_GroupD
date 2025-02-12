﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool IsMoving => _isMoving;
    [SerializeField]
    private float Speed = 5.0f;
    private Vector3 initialPosition;
    private float distance = 0f;
    

    private bool _isMoving;
    Rigidbody2D _rigidbody;

    private int score = 2000;
    public static Action<int> OnPlayerMove;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }
  

    public void OnMove(InputValue value)
    {
        // Read value from control, the type depends on what
        // type of controls the action is bound to
        var inputVal = value.Get<Vector2>();
        // ismae, si miras este codigo, quiero que sepas que no se puede ir en diagonal, hace cosas raras, un saludo

        Vector2 velocity = inputVal * Speed;
        _rigidbody.linearVelocity = velocity;
        _rigidbody.freezeRotation = true;

       

        _isMoving = (velocity.magnitude > 0.01f);

        if (_isMoving) LookAt((Vector2)transform.position + velocity);
        else transform.rotation = Quaternion.identity;


        distance += Vector3.Distance(transform.position, initialPosition);
        initialPosition = transform.position;

        OnPlayerMove?.Invoke((int)distance);


    }

    // NOTE: InputSystem: "SaveScore" action becomes "OnSaveScore" method
    public void OnSaveScore()
    {
        // Usage example on how to save score
        PlayerPrefs.SetInt("Score", score);
        score = PlayerPrefs.GetInt("Score");
    }

    private void LookAt(Vector2 targetPosition)
    {
        float angle = 0.0f;
        Vector3 relative = transform.InverseTransformPoint(targetPosition);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }

}
