using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public void save()
    {
        Debug.Log("Saving room...");
        Room room = new Room();
        List<Machine> machineList = new List<Machine>();
        for(GameObject mach in GameObject.FindGameObjectsWithTag("Grabbable"))
        {
            Machine machine = new Machine();
            machine.Name = mach.name;
            Machine.Position = mach.transform.position;
            Machine.Rotation = mach.transform.rotation;
            machineList.Add(machine);
        }
        string json = JsonUtility.ToJson(room);
        DateTime date = new DateTime.Now;
        string fileName = date.Year + " " + date.Month + " " + date.Day + " - " + date.Hour + " " + date.Minute + " " + date.Second + ".json";
        File.WriteAllText(@fileName, json);
    }

    public void load(string name)
    {
        Debug.Log("Loading setup: " + name + "...");
        string filepath = Path.Combine(Application.streamingAssetsPath, name + ".json");

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
}
