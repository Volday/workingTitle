using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float m_Speed = 12f;

    private string m_MovementForwardAxisName;
    private string m_MovementRightAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementForwardInputValue;
    private float m_MovementRightInputValue;

    private void Awake()
    {
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
        Vector3 movement = transform.forward * m_MovementForwardInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    private void MoveRight()
    {
        Vector3 movement = transform.right * m_MovementRightInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    
}
