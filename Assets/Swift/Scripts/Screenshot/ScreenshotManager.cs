using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;

public class ScreenshotManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Starting screenshot...");
            captureScreenshot();
        }
        
    }

    public void captureScreenshot()
    {
        DateTime date = DateTime.Now;
        string filename =  date.Year + "-" + date.Month + "-" + date.Day + " " + date.Hour + "-" + date.Minute + "-" + date.Second;
        string path = Application.streamingAssetsPath + "/Screenshots/"
                + filename + ".png";
        Debug.Log("Screenshot path: " + path);

        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
        //Get Image from screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();
        //Convert to png
        byte[] imageBytes = screenImage.EncodeToPNG();

        //Save image to file
        System.IO.File.WriteAllBytes(path, imageBytes);
    }
}
