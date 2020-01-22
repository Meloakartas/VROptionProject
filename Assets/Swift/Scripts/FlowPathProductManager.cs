using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.Newtonsoft.Json;

public class FlowPathProductManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public Color color;
    public string[] OrderedMachinesNames;
    private GameObject flowPathInstance;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.transform.Find("Text").gameObject.GetComponent<Text>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("lol");
        if(flowPathInstance != null)
        {
            gameObject.transform.Find("Distance").gameObject.GetComponent<Text>().text = flowPathInstance.GetComponent<FlowPathManager>().GetTotalDistance().ToString();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (flowPathInstance != null && GameObject.Find(flowPathInstance.name) != null)
        {
            Debug.Log("INSTANCE != null & : " + flowPathInstance.name);
            PhotonNetwork.Destroy(flowPathInstance);

            flowPathInstance = null;
        }
        else
        {
            GameObject currentFlowPath = GameObject.Find(gameObject.name + "Arrows");

            if (currentFlowPath == null)
            {
                object[] instanceData = new object[3];

                instanceData[0] = JsonConvert.SerializeObject(OrderedMachinesNames);
                Debug.Log(instanceData[0]);
                instanceData[1] = string.Format("#{0}", ColorUtility.ToHtmlStringRGBA(color));
                instanceData[2] = gameObject.name + "Arrows";
                flowPathInstance = PhotonNetwork.Instantiate("Prefabs/FlowPath", new Vector3(0, 0, 0), Quaternion.identity, 0, instanceData);
            }
            else
            {
                currentFlowPath.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                PhotonNetwork.Destroy(currentFlowPath);

                flowPathInstance = null;
            }
        }
    }

    [PunRPC]
    void SendInstantiationNotification(string productName)
    {

    }

    void IOnEventCallback.OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if ((string)photonEvent.CustomData == gameObject.name + "Arrows")
        {
            flowPathInstance = GameObject.Find((string)photonEvent.CustomData);
        }
    }
}
