using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Networking;

public class ShowConfigs : MonoBehaviour
{
    public GameObject ConfigPreview;

    private readonly string _SUBPATH = "/Swift/StreamingAssets/SavedLayout/";
    private readonly string _IMAGESUBPATH = "/Swift/StreamingAssets/SavedLayoutScreenshots/";

    // Start is called before the first frame update
    void Start()
    {
    
    }

    private void OnEnable()
    {
        UpdateConfigs();
    }

    public void UpdateConfigs()
    {
        var path = new DirectoryInfo(Application.streamingAssetsPath + "/SavedLayout/");
        var configFiles = path.GetFiles();
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (FileInfo file in configFiles)
        {
            if (!file.Name.Contains(".meta"))
            {
                GameObject newConfig = Instantiate(ConfigPreview, gameObject.transform);
                newConfig.GetComponentInChildren<UnityEngine.UI.Text>().text = file.Name.Replace(".json", "");
                var url = Application.streamingAssetsPath + "/SavedLayoutScreenshots/" + file.Name.Replace(".json", ".png").Replace("Swift", "Screen");
                //Application.dataPath + "/Swift/StreamingAssets/SavedLayoutScreenshots/" + file.Name.Replace(".json", ".png");
                //var fileImage = new DirectoryInfo(Application.dataPath + "/Swift/StreamingAssets/SavedLayoutScreenshots/");
                byte[] imgData;
                Texture2D tex = new Texture2D(2, 2);

                imgData = File.ReadAllBytes(url);

                tex.LoadImage(imgData);
                if (url != "")
                {
                    //Texture2D tex = new Texture2D(2, 2);
                    //tex.LoadImage(imagePath.bytes)
                    //Texture2D imagePreview = Resources.Load<Texture2D>(imagePath);
                    newConfig.GetComponentInChildren<UnityEngine.UI.RawImage>().texture = tex;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ToolManager>() != null)
        {

        }
    }

    private string GetPreviewImage(string filepath)
    {
        if (File.Exists(filepath))
        {
            Debug.Log("File exist");
            string json = File.ReadAllText(filepath);
            Debug.Log(json);
            Room room = JsonUtility.FromJson<Room>(json);
            return room.ImagePath;
        }
        return "";
    }
}
