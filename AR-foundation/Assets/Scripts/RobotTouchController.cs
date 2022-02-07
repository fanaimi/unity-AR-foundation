
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

    [SerializeField] private GameObject m_Flames;
    [SerializeField] private GameObject m_Shield;
    

    private bool m_ThrowingFlames = false;

    private Vector3 m_originalPosition; 
    
    
    private Button m_jumpButton;
    private Button m_shieldButton;
    private Button m_flameButton;
    private Button m_repositionButton;

    
    public bool m_IsShieldActive = false;

    // private bool m_jumping  = false;

    // will be triggered when the object is instantiated 
    private void OnEnable()
    {
        m_joystick = FindObjectOfType<Joystick>();
        m_robotAnim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody>();

        SetUpButtons();
        m_originalPosition = transform.position;
    }

    private void SetUpButtons()
    {
        m_jumpButton      = Buttons.Instance.m_jumpButton;
        m_shieldButton     = Buttons.Instance.m_shieldButton;
        m_flameButton      = Buttons.Instance.m_flameButton;
        m_repositionButton = Buttons.Instance.m_repositionButton;
        
        m_jumpButton.onClick.AddListener(Jump);
        m_shieldButton.onClick.AddListener(AddShield);
        m_flameButton.onClick.AddListener(ToggleFlames);
        m_repositionButton.onClick.AddListener(Reposition);
    } // SetUpButtons


    // Update is called once per frame
    void FixedUpdate()
    {
        
       // Debug.Log(transform.position.y);
        // Debug.Log("123");
        // hamdling movements - Joystick magnitude > dead zone
        if (m_joystick.Direction.magnitude > m_deadZone)
        {
            //Debug.Log(transform.position.y);
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

        ThrowFlames();

    }


    public void Jump()
    {
        //DebugManager.Instance.Echo("jump button was pressed");
        
        // DebugManager.Instance.Echo("jumping");
        m_rb.AddForce(transform.up * m_jumpSpeed, ForceMode.Impulse);
    }

    private void AddShield()
    {
        m_IsShieldActive = !m_IsShieldActive;
        //DebugManager.Instance.Echo("shielding");
        m_Shield.SetActive(m_IsShieldActive);
    } // AddShield
    
    
    private void Reposition()
    {
        DebugManager.Instance.Echo("Reposition");
        transform.position = m_originalPosition;
    } // Accelerate
    
    
    private void ToggleFlames()
    {
        // https://www.youtube.com/results?search_query=unity+flamethrower+particle+effect+
        // DebugManager.Instance.Echo("throwing flames");
        m_ThrowingFlames = !m_ThrowingFlames;
    } // ToggleFlames


    private void ThrowFlames()
    {
        if (m_ThrowingFlames)
        {
            // DebugManager.Instance.Echo("throwing flames");
            m_Flames.SetActive(true);
        }
        else
        {
            // DebugManager.Instance.Echo("stopping flames");
            m_Flames.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.LoseLives();
    }
}

