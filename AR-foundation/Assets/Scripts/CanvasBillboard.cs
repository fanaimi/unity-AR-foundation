using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBillboard : MonoBehaviour
{

    private Transform m_Cam;

    private void OnEnable()
    {
        m_Cam = FindObjectOfType<Camera>().transform;
    }

    // LateUpdate is called after every Update call 
    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Cam.forward); 
    }
}
