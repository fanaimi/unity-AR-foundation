using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    
    
    private static Buttons _instance;
    public static Buttons Instance { get { return _instance; } }

    public Button m_jumpButton;
    public Button m_shieldButton;
    public Button m_flameButton;
    public Button m_repositionButton;
    
    
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
}
