using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.Newtonsoft.Json;

public class FlowPathProductManager : MonoBehaviourPunCallbacks
{
    public Color color;
    public string[] OrderedMachinesNames;
    private GameObject flowPathInstance;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<Text>().color = color;
        gameObject.GetComponentInChildren<Button>().image.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (flowPathInstance != null)
            PhotonNetwork.Destroy(flowPathInstance);
        else
        {
            object[] instanceData = new object[2];

            instanceData[0] = JsonConvert.SerializeObject(OrderedMachinesNames);
            instanceData[1] = string.Format("#{0}", ColorUtility.ToHtmlStringRGBA(color));
            flowPathInstance = PhotonNetwork.Instantiate("Prefabs/FlowPath", new Vector3(0, 0, 0), Quaternion.identity, 0, instanceData);
        }
    }
}
