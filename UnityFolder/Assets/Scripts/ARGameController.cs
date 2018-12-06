using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manager script
/// </summary>

namespace ARExample
{

    /// <summary>
    /// singleton script for controlling Rocket model
    /// </summary>
   
    public class ARGameController : MonoBehaviour
    {

        [HideInInspector]
        public GameObject SpawnedRocket;

        // Button to fire up the Rocket
        public GameObject launchButton;
        // joystick to control the direction of the Rocket
        //public GameObject joystick;

        // Private
        private bool isLaunching;
        private float firePower;

        // Singleton
        public static ARGameController Instance;
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            launchButton.SetActive(false);
            firePower = 0; // rocket is not launching
        }

        /// <summary>
        /// if a button is pressed, the rocket will gain power
        /// if a button is released, the rocket will lose power
        /// </summary>
        void FixedUpdate()
        {
            if (SpawnedRocket)
            {
                if (isLaunching)
                {
                    firePower += 0.1f;
                    if (firePower > 10)
                        firePower = 10;
                    ManipulateRocket();
                }
                else
                {
                    firePower -= 0.05f;
                    if (firePower < 9) // always some power once launch starts
                        firePower = 9;
                    ManipulateRocket();
                }
            }
        }

        private void ManipulateRocket()
        {
            // for now, it is only moving upward
            SpawnedRocket.GetComponent<Rigidbody>().AddForce(transform.up * firePower);

            // NEXT STEP - implement joystick to control and move rocket around the scanned space
        }

        /// <summary>
        /// gain power when the button is pressed
        /// </summary>
        public void PowerUp()
        {
            isLaunching = true;
        }
        /// <summary>
        /// lose poewr when the button is released
        /// </summary>
        public void PowerDown()
        {
            isLaunching = false;
        }

        /// <summary>
        /// show controller UI
        /// </summary>
        public void PrepareRocketControllerUI()
        {
            launchButton.SetActive(true);
        }
    }
}
