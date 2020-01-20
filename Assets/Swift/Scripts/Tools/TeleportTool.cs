using Photon.Pun;
using UnityEngine;
using Valve.VR;

public class TeleportTool : MonoBehaviour
{
    public GameObject VRCamera;
    public GameObject CameraRig;
    private GameObject controller;
    

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
    }

    public void TeleportPressed()
    {
        if (!controller.GetComponent<ControllerPointer>())
        {
            controller.AddComponent<ControllerPointer>();
            controller.GetComponent<ControllerPointer>().UpdateColor(Color.green);
        }
    }

    public void TeleportReleased()
    {
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
}
