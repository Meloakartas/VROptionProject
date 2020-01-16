using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ToolManager : MonoBehaviour
{
    public string CurrentTool;
    public GameObject Menu;

    private string toolOnTrackpad;
    private bool isMenuActive;
    private GameObject cameraUser;
    private Material newMat;

    // Start is called before the first frame update
    void Start()
    {
        cameraUser = gameObject.transform.parent.transform.Find("Camera").gameObject;
        isMenuActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(SteamVR_Actions._default.TopView.GetStateUp(SteamVR_Input_Sources.Any))
        {   
            print("ACTIVE/DEACTIVE MENU");
            Menu.SetActive(!isMenuActive);
            isMenuActive = !isMenuActive;
            Vector3 menuPosition = new Vector3(cameraUser.transform.localPosition.x,
            cameraUser.transform.localPosition.y - 0.3f,
            cameraUser.transform.localPosition.z + 0.5f);
            Menu.transform.localPosition = menuPosition;
            Menu.transform.LookAt(2 * Menu.transform.position - cameraUser.transform.position);
        }

        if (CurrentTool == toolOnTrackpad)
            return;

        //switch (CurrentTool)
        //{
        //    case "Grab":
        //        newMat = Resources.Load("GrabHand", typeof(Material)) as Material;
        //        toolOnTrackpad = CurrentTool;
        //        break;
        //    case "TopView":
        //        newMat = Resources.Load("TopViewHand", typeof(Material)) as Material;
        //        toolOnTrackpad = CurrentTool;
        //        break;
        //    case "Snap":
        //        newMat = Resources.Load("SnapHand", typeof(Material)) as Material;
        //        toolOnTrackpad = CurrentTool;
        //        break;
        //    case "SaveConfig":
        //        newMat = Resources.Load("SaveHand", typeof(Material)) as Material;
        //        toolOnTrackpad = CurrentTool;
        //        break;
        //    case "LoadConfig":
        //        newMat = Resources.Load("LoadHand", typeof(Material)) as Material;
        //        toolOnTrackpad = CurrentTool;
        //        break;
        //}
        //if(gameObject.transform.GetChild(0).transform.Find("trackpad") != null)
        //gameObject.transform.GetChild(0).transform.Find("trackpad").gameObject.GetComponent<Renderer>().material = newMat;
    }
}
