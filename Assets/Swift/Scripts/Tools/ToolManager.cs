using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ToolManager : MonoBehaviour
{
    public string CurrentTool;
    public GameObject Menu;
    private bool isMenuActive;
    private GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = gameObject.transform.parent.transform.Find("Camera").gameObject;
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
            Vector3 menuPosition = new Vector3(camera.transform.localPosition.x, 
            camera.transform.localPosition.y - 0.3f,
            camera.transform.localPosition.z + 0.5f);
            Menu.transform.localPosition = menuPosition;
            Menu.transform.LookAt(2 * Menu.transform.position - camera.transform.position);
        }
    }
}
