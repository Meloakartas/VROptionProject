using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SelectTool : MonoBehaviour
{    
    private string currentTool;
    private Material newMat;

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
                    Destroy(other.gameObject.GetComponent<SnapTool>());
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
                    newMat = Resources.Load("Materials/Tools/GrabHand", typeof(Material)) as Material;
                    break;
                case "TopView":
                    other.gameObject.AddComponent<TopviewTool>();
                    other.gameObject.GetComponent<ToolManager>().CurrentTool = "TopView";
                    newMat = Resources.Load("Materials/Tools/TopViewHand", typeof(Material)) as Material;
                    break;
                case "Snap":
                    other.gameObject.AddComponent<SnapTool>();
                    other.gameObject.GetComponent<ToolManager>().CurrentTool = "Snap";
                    newMat = Resources.Load("Materials/Tools/SnapHand", typeof(Material)) as Material;
                    break;
                case "SaveConfig":
                    other.gameObject.AddComponent<GrabTool>();
                    other.gameObject.GetComponent<ToolManager>().CurrentTool = "SaveConfig";
                    newMat = Resources.Load("Materials/Tools/SaveHand", typeof(Material)) as Material;
                    break;
                case "LoadConfig":
                    other.gameObject.AddComponent<GrabTool>();
                    other.gameObject.GetComponent<ToolManager>().CurrentTool = "LoadConfig";
                    newMat = Resources.Load("Materials/Tools/LoadHand", typeof(Material)) as Material;
                    break;
            }
            if(other.gameObject.transform.GetChild(0).transform.Find("trackpad") != null)
                other.gameObject.transform.GetChild(0).transform.Find("trackpad").gameObject.GetComponent<Renderer>().material = newMat;
        }
    }
}
