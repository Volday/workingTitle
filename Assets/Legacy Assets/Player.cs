using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    float m_Speed;

    private MoveSpeed moveSpeed;
    private string m_MovementForwardAxisName;
    private string m_MovementRightAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementForwardInputValue;
    private float m_MovementRightInputValue;

    private void Awake()
    {
        moveSpeed = GetComponent<MoveSpeed>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementForwardInputValue = 0f;
        m_MovementRightInputValue = 0f;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    void Start() {
        
        m_MovementForwardAxisName = "Vertical";
        m_MovementRightAxisName = "Horizontal";
    }

    void Update() {
        m_Speed = moveSpeed.moveSpeed;
        m_MovementForwardInputValue = Input.GetAxis(m_MovementForwardAxisName);
        m_MovementRightInputValue = Input.GetAxis(m_MovementRightAxisName);
    }

    private void FixedUpdate()
    {
        MoveForward();
        MoveRight();
    }

    private void MoveForward()
    {
        Vector3 movement = new Vector3(0, 0, 1) * m_MovementForwardInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    private void MoveRight()
    {
        Vector3 movement = new Vector3(1, 0, 0) * m_MovementRightInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    
}
