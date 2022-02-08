using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : MonoBehaviour
{
    // https://www.youtube.com/watch?v=BLfNP4Sc_iA&ab_channel=Brackeys
    [SerializeField] private float m_turnSpeed = 40f;
    [SerializeField] private Rigidbody m_canonBallPrefab;
    [SerializeField] private ParticleSystem m_MediumFlamesPrefab;
    [SerializeField] private Transform m_spawnPoint;
    [SerializeField] private float m_shootingForce;
    [SerializeField] private int m_FlameDamage = 1;
    [SerializeField] private GameObject m_TowerHealthCanvas;
    private HealthBar m_TowerHealthBar;
  
    private int m_MaxHealth;
    private int m_CurrentHealth;
    
    void OnEnable()
    {
        m_MaxHealth = 1000;
        m_CurrentHealth = m_MaxHealth;
        // m_TowerHealthBar = FindObjectOfType<HealthBar>();
        // m_TowerHealthBar.SetMaxHealth(m_MaxHealth);
        InvokeRepeating("ShootAtPlayer", 3f, 5f);
    } // OnEnable

    
    
    void Update()
    {
        RotateTowardsPlayer();
        
    } // Update

    
    /// <summary>
    /// shooting cannon balls towards player
    /// </summary>
    private void  ShootAtPlayer()
    {
        if (GetRobotPlayer())
        {
            Rigidbody m_canonBall = Instantiate(m_canonBallPrefab, m_spawnPoint.position, m_spawnPoint.rotation);
            m_canonBall.AddForce(m_canonBall.transform.forward * m_shootingForce);
            Destroy(m_canonBall.gameObject, 3f);
        }
    } // ShootAtPlayer


    /// <summary>
    /// rotating tower around Y axis towards the player
    /// </summary>
    private void RotateTowardsPlayer()
    {
        if (!GetRobotPlayer())
        {
            return;
        }
        else
        {
            Vector3 m_targetDirection = GetRobotPlayer().transform.position - transform.position;
            Quaternion m_lookRotation = Quaternion.LookRotation(m_targetDirection);
            Vector3 m_rotation =
                Quaternion.Lerp(
                    transform.rotation,
                    m_lookRotation,
                    Time.deltaTime * m_turnSpeed
                ).eulerAngles;
            
            transform.rotation = Quaternion.Euler(0f, m_rotation.y, 0f);
        }
    } // RotateTowardsPlayer


    /// <summary>
    /// finding robot player in scene
    /// </summary>
    /// <returns>m_robotPlayer</returns>
    private GameObject GetRobotPlayer()
    {
        RobotTouchController m_robotPlayer = FindObjectOfType<RobotTouchController>();

        if (m_robotPlayer)
        {
            return m_robotPlayer.gameObject;
        }
        else
        {
            return default;
        }
    } // GetRobotPlayer


    public void HitByFlame()
    {
        // DebugManager.Instance.Echo("hit by flame!");
        if (!m_TowerHealthCanvas.activeSelf)
        //if (true)
        {
            m_TowerHealthCanvas.SetActive(true);
            m_TowerHealthBar = FindObjectOfType<HealthBar>();
            m_TowerHealthBar.SetMaxHealth(m_MaxHealth);
        }

        m_CurrentHealth -= m_FlameDamage;
        m_TowerHealthBar.SetHealth(m_CurrentHealth);
        if (m_CurrentHealth <= 0)
        {
            AudioManager.instance.Play("tower-smash");
            Destroy(gameObject);
            Instantiate(m_MediumFlamesPrefab, transform.position, transform.rotation);
            AudioManager.instance.Play("camp-fie");
        }
    }

   
}
