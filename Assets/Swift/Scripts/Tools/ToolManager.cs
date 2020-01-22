using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ToolManager : MonoBehaviour
{
    public string CurrentTool;
    public GameObject Menu;
    private SteamVR_Input_Sources inputSource;

    private GameObject cameraUser;

    // Start is called before the first frame update
    void Start()
    {
        inputSource = gameObject.GetComponent<SteamVR_Behaviour_Pose>().inputSource;
        cameraUser = gameObject.transform.parent.transform.Find("Camera").gameObject;
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
            //TODO: Display Cones

            Menu.transform.position = spawnPos;
            Menu.transform.LookAt(2 * Menu.transform.position - cameraUser.transform.position);
        }
    }
}
