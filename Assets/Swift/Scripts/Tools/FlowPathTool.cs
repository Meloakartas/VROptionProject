using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FlowPathTool : MonoBehaviour
{
    private SteamVR_Input_Sources inputSource;
    private GameObject flowPathMenu;
    private GameObject cameraUser;

    // Start is called before the first frame update
    void Start()
    {
        cameraUser = gameObject.transform.parent.transform.Find("Camera").gameObject;
        inputSource = gameObject.GetComponent<SteamVR_Behaviour_Pose>().inputSource;
        flowPathMenu = gameObject.transform.parent.transform.Find("FlowPathMenuManager").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.transform.parent.GetComponent<PhotonView>().IsMine) return;

        if (SteamVR_Actions._default.GrabPinch.GetStateDown(inputSource))
        {
            ShowConfigMenu();
        }
    }

    private void ShowConfigMenu()
    {
        Vector3 playerPos = cameraUser.transform.position;
        Vector3 playerDirection = cameraUser.transform.forward;
        Quaternion playerRotation = cameraUser.transform.rotation;
        float spawnDistance = 0.6f;
        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
        flowPathMenu.transform.position = spawnPos;
        flowPathMenu.transform.LookAt(2 * flowPathMenu.transform.position - cameraUser.transform.position);

        flowPathMenu.SetActive(!flowPathMenu.activeSelf);
    }
}
