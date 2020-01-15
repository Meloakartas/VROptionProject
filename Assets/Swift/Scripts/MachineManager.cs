using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    public GameObject FreeBorder;
    public Material isElectrifiedMaterial;
    public Material isNotElectrifiedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    void ChangeBordersColor(bool mustHighlight, PhotonMessageInfo info)
    {
        Material material = mustHighlight ? isElectrifiedMaterial : isNotElectrifiedMaterial;
        Debug.Log("Must change borders colors");

        foreach (Transform child in FreeBorder.transform)
        {
            child.gameObject.GetComponent<Renderer>().material = material;
        }
    }
}
