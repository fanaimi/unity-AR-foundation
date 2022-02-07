using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{

    private CanonTower m_CanonTower;
    
    void OnParticleTrigger()
    {
        m_CanonTower = FindObjectOfType<CanonTower>();
        m_CanonTower.HitByFlame();
    }

   
}
