using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : MonoBehaviour
{

    [SerializeField] private float m_turnSpeed = 40f;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsPlayer();
    }


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
                    transform.position,
                    m_lookRotation,
                    Time.deltaTime * m_turnSpeed).eulerAngles;
        }
    } // RotateTowardsPlayer


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


}
