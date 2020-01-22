using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SelectTool : MonoBehaviour
{    
    private Material newMat;

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tool")
        {
            switch (gameObject.transform.parent.gameObject.GetComponent<ToolManager>().CurrentTool)
            {
                case "Grab":
                    Destroy(gameObject.transform.parent.gameObject.GetComponent<GrabTool>());
                    break;
                case "TopView":
                    Destroy(gameObject.transform.parent.gameObject.GetComponent<TopviewTool>());
                    break;
                case "Snap":
                    Destroy(gameObject.transform.parent.gameObject.GetComponent<SnapTool>());
                    break;
                case "SaveConfig":
                    Destroy(gameObject.transform.parent.gameObject.GetComponent<ConfigTool>());
                    break;
                case "LoadConfig":
                    Destroy(gameObject.transform.parent.gameObject.GetComponent<ConfigTool>());
                    break;
                case "FlowPath":
                    Destroy(gameObject.transform.parent.gameObject.GetComponent<FlowPathTool>());
                    break;
            }
            AddTool(other.gameObject.name);
        }
    }

    private void AddTool(string tool)
    {
        switch (tool)
        {
            case "Grab":
                gameObject.transform.parent.gameObject.AddComponent<GrabTool>();
                gameObject.transform.parent.gameObject.GetComponent<ToolManager>().CurrentTool = "Grab";
                newMat = Resources.Load("Materials/Tools/GrabHand", typeof(Material)) as Material;
                break;
            case "TopView":
                gameObject.transform.parent.gameObject.AddComponent<TopviewTool>();
                gameObject.transform.parent.gameObject.GetComponent<ToolManager>().CurrentTool = "TopView";
                newMat = Resources.Load("Materials/Tools/TopViewHand", typeof(Material)) as Material;
                break;
            case "Snap":
                gameObject.transform.parent.gameObject.AddComponent<SnapTool>();
                gameObject.transform.parent.gameObject.GetComponent<ToolManager>().CurrentTool = "Snap";
                newMat = Resources.Load("Materials/Tools/SnapHand", typeof(Material)) as Material;
                break;
            case "SaveConfig":
                gameObject.transform.parent.gameObject.AddComponent<ConfigTool>();
                gameObject.transform.parent.gameObject.GetComponent<ToolManager>().CurrentTool = "SaveConfig";
                newMat = Resources.Load("Materials/Tools/SaveHand", typeof(Material)) as Material;
                break;
            case "LoadConfig":
                gameObject.transform.parent.gameObject.AddComponent<ConfigTool>();
                gameObject.transform.parent.gameObject.GetComponent<ToolManager>().CurrentTool = "LoadConfig";
                newMat = Resources.Load("Materials/Tools/LoadHand", typeof(Material)) as Material;
                break;
            case "FlowPath":
                gameObject.transform.parent.gameObject.AddComponent<FlowPathTool>();
                gameObject.transform.parent.gameObject.GetComponent<ToolManager>().CurrentTool = "FlowPath";
                newMat = Resources.Load("Materials/Tools/FlowPathHand", typeof(Material)) as Material;
                break;
        }
        if (gameObject.transform.parent.transform.Find("Model").transform.Find("trackpad") != null)
        {
            gameObject.transform.parent.transform.Find("Model").transform.Find("trackpad").gameObject.GetComponent<Renderer>().material = newMat;
        }
    }
}
