using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using System;
using Valve.VR;

public class ConfigTool : MonoBehaviour
{
    private readonly string _SUBPATH = "/Swift/StreamingAssets/SavedLayout/";
    private SnapTool snapTool;
    private SteamVR_Input_Sources inputSource;
    public string LoadConfig;
    private GameObject configMenu;
    private GameObject cameraUser;

    void Start()
    {
        cameraUser = gameObject.transform.parent.transform.Find("Camera").gameObject;
        inputSource = gameObject.GetComponent<SteamVR_Behaviour_Pose>().inputSource;
        configMenu = GameObject.Find("ConfigMenu");
    }
    void Update()
    {
        if (SteamVR_Actions._default.GrabPinch.GetStateDown(inputSource) && gameObject.GetComponent<ToolManager>().CurrentTool == "SaveConfig")
        {
            Save();
        }
        else if (SteamVR_Actions._default.GrabPinch.GetStateDown(inputSource) && gameObject.GetComponent<ToolManager>().CurrentTool == "LoadConfig")
        {
            if (LoadConfig != "")
                Load(LoadConfig);
            else
            {
                ShowConfigMenu();
            }
        }
    }

    private void ShowConfigMenu()
    {
        Vector3 playerPos = cameraUser.transform.position;
        Vector3 playerDirection = cameraUser.transform.forward;
        Quaternion playerRotation = cameraUser.transform.rotation;
        float spawnDistance = 0.6f;
        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
        configMenu.transform.position = spawnPos;
        configMenu.transform.LookAt(2 * configMenu.transform.position - cameraUser.transform.position);

        configMenu.SetActive(!configMenu.activeSelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Config"))
        {
            LoadConfig = other.GetComponentInChildren<UnityEngine.UI.Text>().text + ".json"; //Config's name
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Config"))
        {
            LoadConfig = "";
        }
    }

    public void Save()
    {
        StartCoroutine(snapTool.captureScreenshot());
        string imagePath = snapTool.path;
        
        Debug.Log(imagePath);

        Room room = new Room();
        List<Machine> machineList = new List<Machine>();

        foreach(GameObject mach in GameObject.FindGameObjectsWithTag("Grabbable"))
        {
            Machine machine = new Machine();
            machine.Name = mach.name;
            machine.Position = mach.transform.position;
            machine.Rotation = mach.transform.rotation;
            machineList.Add(machine);
        }
        room.MachineList = machineList;
        room.ImagePath = imagePath;

        string json = JsonUtility.ToJson(room);
        string filename = formatFilenameWithDate();
        string filepath = Application.dataPath + _SUBPATH + filename;
        File.WriteAllText(filepath, json);

        Debug.Log("Room saved. Path: " + filepath);
    }

    public void Load(string filename)
    {
        string filepath = Application.dataPath + _SUBPATH + filename;

        if(File.Exists(filepath))
        {
            string json = File.ReadAllText(filepath);
            Room room = new Room();
            room = JsonUtility.FromJson<Room>(json);
            foreach(Machine machine in room.MachineList)
            {
                GameObject mach = GameObject.Find(machine.Name);
                mach.transform.position = machine.Position;
                mach.transform.rotation = machine.Rotation;
            }
        }
    }

    private string formatFilenameWithDate()
    {
        DateTime date = DateTime.Now;
        return "Swift-"
        + date.Year + "-" 
        + date.Month + "-" 
        + date.Day + " " 
        + date.Hour + "-" 
        + date.Minute + "-" 
        + date.Second + ".json";
    }
}
