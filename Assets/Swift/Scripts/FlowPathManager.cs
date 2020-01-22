using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.Newtonsoft.Json;

public class FlowPathManager : MonoBehaviour, IPunInstantiateMagicCallback
{
    private List<GameObject> orderedObjects = new List<GameObject>();
    private Color materialColor;
    private GameObject flowPointEntry, flowPointExit;
    private string instanceName = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetTotalDistance()
    {
        float distance = 0f;
        foreach (Transform to in gameObject.transform)
        {
            distance += to.gameObject.GetComponent<ArrowRendererBehavior>().Distance;
        }
        return distance;
    }

    public void AtInstantiation()
    {
        flowPointEntry = GameObject.Find("FlowPointEntry");
        flowPointExit = GameObject.Find("FlowPointExit");

        Debug.Log(flowPointEntry.name + " TO " + orderedObjects.First().gameObject.name);

        DrawArrowBetweenObjects(flowPointEntry, orderedObjects.First().gameObject.transform.Find("FlowPoints").gameObject.transform.Find("EntryPoint").gameObject);

        for(int i = 0; i < orderedObjects.Count - 1; i++)
        {
            Debug.Log(orderedObjects[i].name + " TO " + orderedObjects[i + 1].name);
            DrawArrowBetweenObjects(orderedObjects[i].transform.Find("FlowPoints").gameObject.transform.Find("ExitPoint").gameObject, orderedObjects[i+1].transform.Find("FlowPoints").gameObject.transform.Find("EntryPoint").gameObject);
        }

        Debug.Log(orderedObjects.Last().gameObject.name + " TO " + flowPointExit.name);
        DrawArrowBetweenObjects(orderedObjects.Last().gameObject.transform.Find("FlowPoints").gameObject.transform.Find("ExitPoint").gameObject, flowPointExit);
    }

    private void DrawArrowBetweenObjects(GameObject objectFrom, GameObject objectTo)
    {
        GameObject gObject = new GameObject("Line");
        gObject.transform.SetParent(gameObject.transform);
        LineRenderer lRend = gObject.AddComponent<LineRenderer>();
        gObject.AddComponent<ArrowRendererBehavior>();
        gObject.GetComponent<ArrowRendererBehavior>().ObjectFrom = objectFrom;
        gObject.GetComponent<ArrowRendererBehavior>().ObjectTo = objectTo;

        lRend.widthCurve = new AnimationCurve(
             new Keyframe(0, 0.4f)
             , new Keyframe(0.999f - 0.4f, 0.4f)  // neck of arrow
             , new Keyframe(1 - 0.4f, 1f)  // max width of arrow head
             , new Keyframe(1, 0f));  // tip of arrow

        lRend.material.color = materialColor;
        lRend.SetPosition(0, objectFrom.transform.position);
        lRend.SetPosition(1, objectTo.transform.position);
        //TODO; using material
    }

    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] data = gameObject.GetComponent<PhotonView>().InstantiationData;
        ColorUtility.TryParseHtmlString(data[1].ToString(), out materialColor);
        Debug.Log("INSTANTIATING : " + data[2].ToString());
        gameObject.name = data[2].ToString();

        string content = gameObject.name; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(0, content, raiseEventOptions, sendOptions);

        foreach (string s in JsonConvert.DeserializeObject<List<string>>(data[0].ToString()))
        {
            orderedObjects.Add(GameObject.Find(s));
        }

        AtInstantiation();
    }
}
