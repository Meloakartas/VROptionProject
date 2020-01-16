using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShowConfigs : MonoBehaviour
{
    public GameObject ConfigPreview;

    private readonly string _SUBPATH = "/Swift/StreamingAssets/SavedLayout/";

    // Start is called before the first frame update
    void Start()
    {
        var path = new DirectoryInfo(Application.dataPath + _SUBPATH);
        var configFiles = path.GetFiles();
        foreach (FileInfo file in configFiles)
        {
            if(file.Name.Contains(".json"))
            {
                GameObject newConfig = Instantiate(ConfigPreview, gameObject.transform);
                newConfig.GetComponentInChildren<UnityEngine.UI.Text>().text = file.Name.Replace(".json","");
                var imagePath = GetPreviewImage(path + file.Name);
                if (imagePath != "")
                {
                    Texture imagePreview = Resources.Load(imagePath, typeof(Texture)) as Texture;
                    newConfig.GetComponentInChildren<UnityEngine.UI.RawImage>().texture = imagePreview;
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
            string json = File.ReadAllText(filepath);
            Room room = JsonUtility.FromJson<Room>(json);
            return room.ImagePath;
        }
        return "";
    }
}
