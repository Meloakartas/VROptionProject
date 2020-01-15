using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ToolManager : MonoBehaviour
{
    public string CurrentTool;
    public GameObject Menu;
    private bool isMenuActive;

    // Start is called before the first frame update
    void Start()
    {
        isMenuActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(SteamVR_Actions._default.TopView.GetStateUp(SteamVR_Input_Sources.LeftHand))
        {
            Menu.SetActive(!isMenuActive);
            isMenuActive = !isMenuActive;
        }
    }
}
