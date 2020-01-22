using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ToolManager : MonoBehaviour
{
    public string CurrentTool;
    public GameObject Menu;
    private SteamVR_Input_Sources inputSource;
    public GameObject[] ToolPicker;
    private Material newMat;

    private GameObject cameraUser;

    // Start is called before the first frame update
    void Start()
    {
        inputSource = gameObject.GetComponent<SteamVR_Behaviour_Pose>().inputSource;
        cameraUser = gameObject.transform.parent.transform.Find("Camera").gameObject;
        AddDefaultTool();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.transform.parent.GetComponent<PhotonView>().IsMine) return;

        if (SteamVR_Actions._default.MenuButton.GetStateDown(inputSource))
        {
            Vector3 playerPos = cameraUser.transform.position;
            Vector3 playerDirection = cameraUser.transform.forward;
            Quaternion playerRotation = cameraUser.transform.rotation;
            float spawnDistance = 0.6f;

            Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
            Menu.SetActive(!Menu.activeSelf);
            foreach(var tool in ToolPicker)
            {
                tool.SetActive(Menu.activeSelf);
            }

            Menu.transform.position = spawnPos;
            Menu.transform.LookAt(2 * Menu.transform.position - cameraUser.transform.position);
        }
    }

    private void AddDefaultTool()
    {
        CurrentTool = "Grab";
        gameObject.AddComponent<GrabTool>();
        newMat = Resources.Load("Materials/Tools/GrabHand", typeof(Material)) as Material;
        if (gameObject.transform.GetChild(0).transform.Find("trackpad") != null)
        {
            gameObject.transform.GetChild(0).transform.Find("trackpad").gameObject.GetComponent<Renderer>().material = newMat;
        }
    }
}
