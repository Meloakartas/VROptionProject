using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;
using Valve.VR;
using Photon.Pun;

public class SnapTool : MonoBehaviour
{

    private IEnumerator coroutine;
    public string path;
    private SteamVR_Input_Sources inputSource;
    void Start()
    {
        inputSource = gameObject.GetComponent<SteamVR_Behaviour_Pose>().inputSource;
    }

    void Update()
    {
        if (!gameObject.transform.parent.GetComponent<PhotonView>().IsMine) return;

        if (SteamVR_Actions._default.GrabPinch.GetStateDown(inputSource))
        {
            coroutine = captureScreenshot(false);
            StartCoroutine(coroutine);
        }
    }

    public IEnumerator captureScreenshot(bool isConfigPreview)
    {
        yield return new WaitForEndOfFrame();

        DateTime date = DateTime.Now;
        string filename = "Screen-" + date.Year + "-" + date.Month + "-" + date.Day + " " + date.Hour + "-" + date.Minute + "-" + date.Second + ".png";
        if(isConfigPreview)
        {
            path = Application.streamingAssetsPath + "/SavedLayoutScreenshots/" + filename;
            //path = Application.dataPath + "/Swift/Resources/SavedLayoutScreenshots/" + filename;
        }
        else
        {
            path = Application.streamingAssetsPath + "/Screenshots/" + filename;
        }

        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);

        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();

        byte[] imageBytes = screenImage.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, imageBytes);
    }
}
