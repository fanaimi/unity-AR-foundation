
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTouchController : MonoBehaviour
{

    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_turnSpeed;
    [SerializeField] private float m_jumpSpeed;
    private float m_deadZone = .2f;
    private Animator m_robotAnim;
    
    private Rigidbody m_rb;
    // [SerializeField] 
    private Joystick m_joystick;

    private bool m_jumping  = false;

    // will be triggered when the object is instantiated 
    private void OnEnable()
    {
        m_joystick = FindObjectOfType<Joystick>();
        m_robotAnim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("joystick moving");
        // hamdling movements - Joystick magnitude > dead zone
        if (m_joystick.Direction.magnitude > m_deadZone)
        {
           m_rb.AddForce(transform.forward * m_moveSpeed);
           m_robotAnim.SetBool("Walk_Anim", true);
        }
        else
        {
            m_robotAnim.SetBool("Walk_Anim", false);
        }

        // handling rotation
        Vector3 m_targetDirection = new Vector3(
            m_joystick.Direction.x, 
            0,
            m_joystick.Direction.y 
        );

        Vector3 m_direction = Vector3.RotateTowards(
            transform.forward, 
            m_targetDirection, 
            Time.deltaTime * m_turnSpeed,
            0.0f
        );
        
        transform.rotation = Quaternion.LookRotation(m_direction);
        

    }


    public void Jump()
    {
        m_jumping = true;
        DebugManager.Instance.Echo("jump button was pressed");
        
        // m_rb.AddForce(transform.up * m_jumpSpeed);
        m_rb.velocity = Vector3.up * 500; 
    }


    private void OnDestroy()
    {
        GameManager.Instance.LoseLives();
    }
}

