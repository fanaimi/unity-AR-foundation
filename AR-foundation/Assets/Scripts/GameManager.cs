using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public Canvas m_gameOverCanvas;
    public int m_lives = 10;

    public TMP_Text m_livesTxt;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        m_gameOverCanvas.gameObject.SetActive(false);
        m_livesTxt.text = m_lives.ToString();
    } // Awake


    public void LoseLives()
    {
        m_lives--;
        m_livesTxt.text = m_lives.ToString();

        if (m_lives <= 0)
        {
            m_gameOverCanvas.gameObject.SetActive(true);
        }

    } // LoseLives


}
