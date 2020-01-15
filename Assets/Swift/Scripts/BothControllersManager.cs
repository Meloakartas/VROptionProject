using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BothControllersManager : MonoBehaviour
{
    private GameObject grabbedObject;
    private SteamVR_Input_Sources inputSource;
    private GameObject controller;

    // Start is called before the first frame update
    void Awake()
    {
        controller = this.gameObject;
        inputSource = controller.GetComponent<SteamVR_Behaviour_Pose>().inputSource;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.transform.parent.GetComponent<PhotonView>().IsMine) return;

        if (SteamVR_Actions._default.GrabPinch.GetStateDown(inputSource) && grabbedObject)
        {
            GrabSelectedObject(controller);
        }
        if (SteamVR_Actions._default.GrabPinch.GetStateUp(inputSource) && grabbedObject)
        {
            UngrabSelectedObject(controller);
        }
    }

    void GrabSelectedObject(GameObject controller)
    {
        grabbedObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
        Debug.Log("Grabbing object : " + grabbedObject.name + " with controller : " + controller.name);
        FixedJoint fx = controller.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        fx.connectedBody = grabbedObject.GetComponent<Rigidbody>();
        foreach(var collider in grabbedObject.GetComponentsInParent<BoxCollider>())
        {
            collider.enabled = false;
        }
    }

    void UngrabSelectedObject(GameObject controller)
    {
        Debug.Log("Ungrabbing object : " + grabbedObject.name + " with controller: " + controller.name);
        FixedJoint fx = controller.GetComponent<FixedJoint>();
        if (fx)
        {
            fx.connectedBody.GetComponent<Rigidbody>().velocity = controller.GetComponent<SteamVR_Behaviour_Pose>().GetVelocity() * 2;
            fx.connectedBody.GetComponent<Rigidbody>().angularVelocity = controller.GetComponent<SteamVR_Behaviour_Pose>().GetAngularVelocity() * 2;
            fx.connectedBody = null;
            Destroy(controller.GetComponent<FixedJoint>());
        }
        //grabbedObject.GetComponentInParent<BoxCollider>().enabled = true;
        foreach (var collider in grabbedObject.GetComponentsInParent<BoxCollider>())
        {
            collider.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Grabbable" && grabbedObject == null)
        {
            Debug.Log("Triggered a grabbable object : " + controller.name);
            grabbedObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    { 
        FixedJoint fx = controller.GetComponent<FixedJoint>();
        if (!fx && other.tag == "Grabbable")
        {
            Debug.Log("Exited a grabbable object : " + controller.name);
            grabbedObject = null;
        }
    }
}
