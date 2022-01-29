
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTouchController : MonoBehaviour
{

    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_turnSpeed;
    private float m_deadZone = .02f;
    private Animator m_robotAnim;
    
    private Rigidbody m_rb;
    // [SerializeField] 
    private Joystick m_joystick;

    private bool active = false;

    // will be triggered when the object is instantiated 
    private void OnEnable()
    {
        m_joystick = FindObjectOfType<Joystick>();
        m_robotAnim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody>();
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("joystick moving");
        // hamdling movements - Joystick magnitude > dead zone
        if (m_joystick.Direction.magnitude > m_deadZone)
        // if (active)
        {
            // Debug.Log("joystick moving");
            DebugManager.Instance.Echo("joystick moving");
            DebugManager.Instance.Echo($"{m_joystick.Direction}");
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
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RobotTouchController : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    public float deadZone = 0.2f;

    private Animator robotAnim;
    private Rigidbody robotRB;
    private Joystick joystick;
    
    void OnEnable()
    {
        joystick = FindObjectOfType<Joystick>();
        robotAnim = GetComponent<Animator>();
        robotRB = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        // handle movement
        if (joystick.Direction.magnitude >= deadZone)
        {
            robotRB.AddForce(transform.forward * moveSpeed);
            robotAnim.SetBool("Walk_Anim", true);
        }
        else
        {
            // idling
            robotAnim.SetBool("Walk_Anim", false);
        }

        // handle rotation
        Vector3 targetDirection = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        Vector3 direction = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * turnSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(direction);
    }
}*/