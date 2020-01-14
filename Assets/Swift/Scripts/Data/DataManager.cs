using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using System;

public class DataManager : MonoBehaviour
{
    void Awake()
    {
        // test purpose
        //Save();
    }

    public void Save()
    {
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

        string json = JsonUtility.ToJson(room);
        string filename = formatFilenameWithDate();
        string filepath = Path.Combine(Application.streamingAssetsPath, filename);
        Debug.Log("FilePath: " + filepath);
        File.WriteAllText(filepath, json);
    }

    public void Load(string filename)
    {
        Debug.Log("Loading setup: " + name + "...");
        string filepath = Path.Combine(Application.streamingAssetsPath, filename);

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
        return date.Year + " " 
        + date.Month + " " 
        + date.Day + " - " 
        + date.Hour + " " 
        + date.Minute + " " 
        + date.Second + ".json";
    }
}
