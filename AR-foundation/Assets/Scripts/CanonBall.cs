using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        DebugManager.Instance.Echo("ball hit something");
        RobotTouchController m_robotPlayer = other.collider.GetComponent<RobotTouchController>();

        if (m_robotPlayer)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    /*private void OnXCollisionEnter(Collision collision)
    {
        
        RobotTouchController m_robotPlayer = collision.collider.GetComponent<RobotTouchController>();

        if (m_robotPlayer)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }*/
}
