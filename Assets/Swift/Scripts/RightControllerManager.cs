using Photon.Pun;
using UnityEngine;
using Valve.VR;

public class RightControllerManager : MonoBehaviourPunCallbacks
{
    private GameObject grabbedObject;
    public GameObject VRCamera;
    public GameObject CameraRig;
    private bool isControllerInside = false;
    private GameObject controller;

    public delegate void OnGrabPressed(GameObject controller);
    public static event OnGrabPressed onGrabPressed;

    public delegate void OnGrabReleased(GameObject controller);
    public static event OnGrabReleased onGrabReleased;

    private bool isOnTopView = false;

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

        if (SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand) && grabbedObject)
        {
            GrabSelectedObject(controller);
        }
        if (SteamVR_Actions._default.GrabPinch.GetStateUp(SteamVR_Input_Sources.RightHand) && grabbedObject)
        {
            UngrabSelectedObject(controller);
        }

        if (SteamVR_Actions._default.TopView.GetStateUp(SteamVR_Input_Sources.RightHand))
        {
            if (isOnTopView)
            {
                Vector3 currentPosition = VRCamera.transform.position;
                currentPosition.y = 0f;
                VRCamera.transform.position = currentPosition;
                CameraRig.transform.position = currentPosition;
            }
            else
            {
                Vector3 currentPosition = VRCamera.transform.position;
                currentPosition.y = GameObject.Find("TopView").gameObject.transform.position.y;
                VRCamera.transform.position = currentPosition;
                CameraRig.transform.position = currentPosition;
            }
            isOnTopView = !isOnTopView;
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
            fx.connectedBody.GetComponent<Rigidbody>().velocity = controller.GetComponent<SteamVR_Behaviour_Pose>().GetVelocity()*2;
            fx.connectedBody.GetComponent<Rigidbody>().angularVelocity = controller.GetComponent<SteamVR_Behaviour_Pose>().GetAngularVelocity()*2;
            fx.connectedBody = null;
            Destroy(controller.GetComponent<FixedJoint>());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Grabbable")
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