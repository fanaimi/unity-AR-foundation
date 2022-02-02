
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    public Button m_jumpButton;
    public Button m_shieldButton;
    public Button m_flameButton;
    public Button m_accelerateButton;

    // private bool m_jumping  = false;

    // will be triggered when the object is instantiated 
    private void OnEnable()
    {
        m_joystick = FindObjectOfType<Joystick>();
        m_robotAnim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody>();

        SetUpButtons();

    }

    private void SetUpButtons()
    {
        m_jumpButton      = Buttons.Instance.m_jumpButton;
        m_shieldButton     = Buttons.Instance.m_shieldButton;
        m_flameButton      = Buttons.Instance.m_flameButton;
        m_accelerateButton = Buttons.Instance.m_accelerateButton;
        
        m_jumpButton.onClick.AddListener(Jump);
        m_shieldButton.onClick.AddListener(AddShield);
        m_flameButton.onClick.AddListener(ThrowFlames);
        m_accelerateButton.onClick.AddListener(Accelerate);
    } // SetUpButtons


    // Update is called once per frame
    void FixedUpdate()
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
        //DebugManager.Instance.Echo("jump button was pressed");
        
        DebugManager.Instance.Echo("jumping");
        m_rb.AddForce(transform.up * m_jumpSpeed, ForceMode.Impulse);
    }

    private void AddShield()
    {
        DebugManager.Instance.Echo("shielding");
    } // AddShield
    
    
    private void Accelerate()
    {
        DebugManager.Instance.Echo("accelerate");
    } // Accelerate
    
    
    private void ThrowFlames()
    {
        // https://www.youtube.com/results?search_query=unity+flamethrower+particle+effect+
        DebugManager.Instance.Echo("throwing flames");
    } // ThrowFlames


    private void OnDestroy()
    {
        GameManager.Instance.LoseLives();
    }
}

