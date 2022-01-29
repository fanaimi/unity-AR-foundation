using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        RobotTouchController m_robotPlayer = collision.collider.GetComponent<RobotTouchController>();

        if (m_robotPlayer)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
