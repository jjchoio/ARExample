using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;


/// <summary>
/// Spawn prefab on a plane by tap on the screen
/// initially learned from the script, https://github.com/Unity-Technologies/arfoundation-samples
/// </summary>

namespace ARExample
{
    [RequireComponent(typeof(ARSessionOrigin))]
    public class SpawnPrefabModel : MonoBehaviour
    {
        // The prefab to instantiate on touch.
        public GameObject rocketPrefab;

        // Visualize where to instantiate the prefab
        public GameObject spawnLocation;

        // Private 
        private bool isSpawned;
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
            // always raycasting when prefab is NOT placed
            // you can instantiate only once
            if (isSpawned == false)
            {
                if (m_SessionOrigin.Raycast(camera_center, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    // spawnLocation follows the center of camera
                    spawnLocation.GetComponentInChildren<Renderer>().enabled = true;
                    spawnLocation.transform.position = hitPose.position;
                    spawnLocation.transform.rotation = hitPose.rotation;

                    // touch to place the prefab & turn off the spawnLocation
                    if (Input.touchCount > 0)
                    {
                        ARGameController.Instance.SpawnedRocket = Instantiate(rocketPrefab, hitPose.position, Quaternion.identity);
                        ARGameController.Instance.PrepareRocketControllerUI();
                        spawnLocation.GetComponentInChildren<Renderer>().enabled = false; // hide the spawnLocation object
                        isSpawned = true;
                    }
                }
                else
                {
                    spawnLocation.GetComponentInChildren<Renderer>().enabled = false;
                }
            }
        }
    }
}