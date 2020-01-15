using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SelectTool : MonoBehaviour
{    
    void Start()
    {
        
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<SteamVR_Behaviour_Pose>() != null)
        {
            Destroy(other.gameObject.GetComponent<BothControllersManager>() ??
            other.gameObject.GetComponent<BothControllersManager>() ??
            other.gameObject.GetComponent<BothControllersManager>() ??
            other.gameObject.GetComponent<BothControllersManager>() ??
            other.gameObject.GetComponent<BothControllersManager>());

            switch (gameObject.name)
            {
                case "Grab":
                    other.gameObject.AddComponent<BothControllersManager>();
                    break;
                case "TopView":
                    other.gameObject.AddComponent<BothControllersManager>();
                    break;
                case "Snap":
                    other.gameObject.AddComponent<BothControllersManager>();
                    break;
                case "SaveConfig":
                    other.gameObject.AddComponent<BothControllersManager>();
                    break;
                case "LoadConfig":
                    other.gameObject.AddComponent<BothControllersManager>();
                    break;
            }
        }
    }
}
