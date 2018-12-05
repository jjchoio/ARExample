using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;


/// <summary>
/// modifed from the script from https://github.com/Unity-Technologies/arfoundation-samples
/// </summary>

[RequireComponent(typeof(ARSessionOrigin))]
public class PlaceOnPlane : MonoBehaviour
{
    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab;
    /// <summary>
    /// Visualize where to instantiate the prefab
    /// </summary>
    public GameObject cursor;

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    private ARSessionOrigin m_SessionOrigin;
    
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private Camera ar_camera;
    private Vector2 camera_center;

    void Awake()
    {
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
    }

    private void Start()
    {
        ar_camera = Camera.main;
        camera_center = new Vector2(ar_camera.pixelWidth / 2, ar_camera.pixelHeight / 2);
    }

    void Update()
    {

        if (m_SessionOrigin.Raycast(camera_center, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = s_Hits[0].pose;
            cursor.transform.position = hitPose.position;

            if (Input.touchCount > 0)
            {
                // you can instantiate only once
                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(placedPrefab, hitPose.position, Quaternion.identity);

                    // turn off the cursor
                    cursor.SetActive(false);
                }
            }
        }

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    if (m_SessionOrigin.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
        //    {
        //        Pose hitPose = s_Hits[0].pose;

        //        if (spawnedObject == null)
        //        {
        //            spawnedObject = Instantiate(placedPrefab, hitPose.position, Quaternion.identity);
        //        }
        //        else
        //        {
        //            spawnedObject.transform.position = hitPose.position;
        //        }
        //    }
        //}
    }
}
