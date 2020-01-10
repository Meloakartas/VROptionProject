using Photon.Pun;
using UnityEngine;
using Valve.VR;

public class LeftControllerManager : MonoBehaviourPunCallbacks
{
    public GameObject VRCamera;
    public GameObject CameraRig;
    private GameObject controller;
    private GameObject grabbedObject;

    public delegate void OnGrabPressed(GameObject controller);
    public static event OnGrabPressed onGrabPressed;

    public delegate void OnGrabReleased(GameObject controller);
    public static event OnGrabReleased onGrabReleased;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        controller = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.transform.parent.GetComponent<PhotonView>().IsMine) return;

        if (SteamVR_Actions._default.Teleport.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            TeleportPressed();
        }

        if (SteamVR_Actions._default.Teleport.GetStateUp(SteamVR_Input_Sources.LeftHand))
        {
            TeleportReleased();
        }

        if (SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand) && grabbedObject)
        {
            GrabSelectedObject(controller);
        }
        if (SteamVR_Actions._default.GrabPinch.GetStateUp(SteamVR_Input_Sources.LeftHand) && grabbedObject)
        {
            UngrabSelectedObject(controller);
        }
    }

    public void TeleportPressed()
    {
        Debug.Log("Teleport pressed");
        if (!controller.GetComponent<ControllerPointer>())
        {
            controller.AddComponent<ControllerPointer>();
            controller.GetComponent<ControllerPointer>().UpdateColor(Color.green);
        }
    }

    public void TeleportReleased()
    {
        Debug.Log("Teleport released");
        if (controller.GetComponent<ControllerPointer>().CanTeleport)
        {
            CameraRig.transform.position = controller.GetComponent<ControllerPointer>().TargetPosition;
            VRCamera.transform.position = controller.GetComponent<ControllerPointer>().TargetPosition;

            controller.GetComponent<ControllerPointer>().DesactivatePointer();
            Destroy(controller.GetComponent<ControllerPointer>());
        }
        else
        {
            controller.GetComponent<ControllerPointer>().DesactivatePointer();
            Destroy(controller.GetComponent<ControllerPointer>());
        }
    }

    void GrabSelectedObject(GameObject controller)
    {
        Debug.Log("Grabbing object with controller : " + controller.name);
        FixedJoint fx = controller.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        fx.connectedBody = grabbedObject.GetComponent<Rigidbody>();
    }

    void UngrabSelectedObject(GameObject controller)
    {
        Debug.Log("Ungrabbing object with controller : " + controller.name);
        FixedJoint fx = controller.GetComponent<FixedJoint>();
        if (fx)
        {
            fx.connectedBody.GetComponent<Rigidbody>().velocity = controller.GetComponent<SteamVR_Behaviour_Pose>().GetVelocity() * 2;
            fx.connectedBody.GetComponent<Rigidbody>().angularVelocity = controller.GetComponent<SteamVR_Behaviour_Pose>().GetAngularVelocity() * 2;
            fx.connectedBody = null;
            Destroy(controller.GetComponent<FixedJoint>());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Grabbable")
        {
            Debug.Log("Triggered a grabbable object : " + controller.name);
            grabbedObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Grabbable")
        {
            Debug.Log("Exited a grabbable object : " + controller.name);
            grabbedObject = null;
        }
    }
}
