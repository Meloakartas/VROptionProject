using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SelectTool : MonoBehaviour
{    
    private ToolManager toolManager;
    private string currentTool;

    void Start()
    {
        
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<ToolManager>() != null)
        {
            currentTool = other.gameObject.GetComponent<ToolManager>().CurrentTool;

            switch (currentTool)
            {
                case "Grab":
                    Destroy(other.gameObject.GetComponent<GrabTool>());
                    break;
                case "TopView":
                    Destroy(other.gameObject.GetComponent<TopviewTool>());
                    break;
                case "Snap":
                    Destroy(other.gameObject.GetComponent<GrabTool>());
                    break;
                case "SaveConfig":
                    Destroy(other.gameObject.GetComponent<GrabTool>());
                    break;
                case "LoadConfig":
                    Destroy(other.gameObject.GetComponent<GrabTool>());
                    break;
            }

            switch (gameObject.name)
            {
                case "Grab":
                    other.gameObject.AddComponent<GrabTool>();
                    other.gameObject.GetComponent<ToolManager>().CurrentTool = "Grab";
                    break;
                case "TopView":
                    other.gameObject.AddComponent<TopviewTool>();
                    other.gameObject.GetComponent<ToolManager>().CurrentTool = "TopView";
                    break;
                case "Snap":
                    other.gameObject.AddComponent<GrabTool>();
                    other.gameObject.GetComponent<ToolManager>().CurrentTool = "Snap";
                    break;
                case "SaveConfig":
                    other.gameObject.AddComponent<GrabTool>();
                    other.gameObject.GetComponent<ToolManager>().CurrentTool = "SaveConfig";
                    break;
                case "LoadConfig":
                    other.gameObject.AddComponent<GrabTool>();
                    other.gameObject.GetComponent<ToolManager>().CurrentTool = "LoadConfig";
                    break;
            }
        }
    }
}
