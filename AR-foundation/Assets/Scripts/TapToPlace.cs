using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class TapToPlace : MonoBehaviour
{

    [SerializeField] private GameObject m_robotPrefab;
    private GameObject m_spawnedRobot;

    private ARRaycastManager m_raycastManager;
    private static List<ARRaycastHit> m_hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // detecting touch on screen
        if (Input.touchCount > 0)
        {
            // saving position of first touch on screen
            Vector2 m_touchPos = Input.GetTouch(0).position;
            
            // spawn a prefab if we don't have one already
            if (m_spawnedRobot == null)
            {
                // we will only raycast on a plane that we detected
                // raycasts start from touched points on the screen
                // we save what raycasts hit on a List named m_hits
                // Trackable is anything that the phone can track
                // PlaneWithinPolygon refers to the planes we have detected
                if ( m_raycastManager.Raycast(m_touchPos, m_hits, TrackableType.PlaneWithinPolygon))
                {
                   
                }
            }
            else
            {
                
            } // spawnedRobot

        }// touchCount
    } // update
}
