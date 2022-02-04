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
            DebugManager.Instance.Echo("Player hit");
            RobotTouchController m_robotPlayer = collision.collider.GetComponent<RobotTouchController>();

            if (m_robotPlayer)
            {
                AudioManager.instance.Play("explosion");
                Instantiate(m_Explosion, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }


        if (collision.collider.CompareTag("Flames"))
        {
            Destroy(gameObject);
        }
    }
}
