using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private TMP_Text m_DebugMsg;
    
    private static DebugManager _instance;
    public static DebugManager Instance { get { return _instance; } }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void Echo(string msg)
    {
        m_DebugMsg.text = msg.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
       // Echo("we are live");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
