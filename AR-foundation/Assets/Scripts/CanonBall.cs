using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    [SerializeField] private GameObject m_Explosion;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            RobotTouchController m_robotPlayer = collision.collider.GetComponent<RobotTouchController>();

            if (m_robotPlayer && !m_robotPlayer.m_IsShieldActive)
            {
                // DebugManager.Instance.Echo("Player hit");
                AudioManager.instance.Play("explosion");
                Instantiate(m_Explosion, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            } else if (m_robotPlayer && m_robotPlayer.m_IsShieldActive)
            {
                AudioManager.instance.Play("buzz");
                Destroy(gameObject);
            }
        }


        if (collision.collider.CompareTag("Flames"))
        {
            Destroy(gameObject);
        }
        
        
    }


}
