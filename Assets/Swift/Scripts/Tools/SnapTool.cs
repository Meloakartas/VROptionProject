using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;
using Valve.VR;

public class SnapTool : MonoBehaviour
{

    private SteamVR_Input_Sources inputSource;
    void Start()
    {
        inputSource = gameObject.GetComponent<SteamVR_Behaviour_Pose>().inputSource;
    }

    void Update()
    {
        if (SteamVR_Actions._default.GrabPinch.GetStateDown(inputSource))
        {
            Debug.Log("Starting screenshot...");
            captureScreenshot();
        }
    }

    public string captureScreenshot()
    {
        DateTime date = DateTime.Now;
        string filename = "Screen-" + date.Year + "-" + date.Month + "-" + date.Day + " " + date.Hour + "-" + date.Minute + "-" + date.Second + ".png";
        string path = Application.dataPath + "/Swift/StreamingAssets/Screenshots/" + filename;
        Debug.Log("Screenshot path: " + path);

        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);

        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();

        byte[] imageBytes = screenImage.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, imageBytes);
        return path;
    }
}
