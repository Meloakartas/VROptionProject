using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityCableBehavior : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ElectricCable")
        {
            Debug.Log("ELECTRICITY ENTER GRABB");
            gameObject.GetComponentInParent<PhotonView>().RPC("ChangeBordersColor", RpcTarget.AllViaServer, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ElectricCable")
        {
            Debug.Log("ELECTRICITY Exit GRABB");
            gameObject.GetComponentInParent<PhotonView>().RPC("ChangeBordersColor", RpcTarget.AllViaServer, false);
        }
    }
}
