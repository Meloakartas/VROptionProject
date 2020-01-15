using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using System;

public class DataManager : MonoBehaviour
{
    private readonly string _SUBPATH = "/Swift/StreamingAssets/SavedLayout/";
    //private ScreenshotManager screenshotManager;

    void Awake()
    {
        //test purpose
        //Save();
        Load("test");
    }

    public void Save()
    {
        Debug.Log("Taking screenshot...");
        string imagePath = "";//screenshotManager.captureScreenshot();
        Debug.Log("Saving room...");
        Room room = new Room();
        List<Machine> machineList = new List<Machine>();
        int i = 0;
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
        //filename = "Swift-2020-1-15 11-28-22.json";
        Debug.Log("Loading setup: " + filename + "...");
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
