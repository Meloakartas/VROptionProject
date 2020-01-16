using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ToolManager : MonoBehaviour
{
    public string CurrentTool;
    public GameObject Menu;

    private GameObject cameraUser;

    // Start is called before the first frame update
    void Start()
    {
        cameraUser = gameObject.transform.parent.transform.Find("Camera").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(SteamVR_Actions._default.TopView.GetStateUp(SteamVR_Input_Sources.Any))
        {
            Vector3 playerPos = cameraUser.transform.position;
            Vector3 playerDirection = cameraUser.transform.forward;
            Quaternion playerRotation = cameraUser.transform.rotation;
            float spawnDistance = 0.6f;

            Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

            Menu.SetActive(!Menu.activeSelf);

            Menu.transform.position = spawnPos;
            Menu.transform.LookAt(2 * Menu.transform.position - cameraUser.transform.position);
        }
    }
}
