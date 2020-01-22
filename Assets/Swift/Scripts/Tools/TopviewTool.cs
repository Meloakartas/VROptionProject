using Photon.Pun;
using UnityEngine;
using Valve.VR;

public class TopviewTool : MonoBehaviour
{ 
    private GameObject grabbedObject;

    private bool isOnTopView = false;

    private bool isControllerInside = false;

    private GameObject VRCamera;
    private GameObject CameraRig;

    private float topviewY;

    private SteamVR_Input_Sources inputSource;

    public delegate void OnGrabPressed(GameObject controller);
    public static event OnGrabPressed onGrabPressed;

    public delegate void OnGrabReleased(GameObject controller);
    public static event OnGrabReleased onGrabReleased;

    // Start is called before the first frame update
    void Start()
    {
        inputSource = gameObject.GetComponent<SteamVR_Behaviour_Pose>().inputSource;
        CameraRig = gameObject.transform.parent.gameObject;
        VRCamera = CameraRig.transform.Find("Camera").gameObject;
        topviewY = GameObject.Find("TopViewFloor").gameObject.transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        if (!gameObject.transform.parent.GetComponent<PhotonView>().IsMine) return;

        if (SteamVR_Actions._default.GrabPinch.GetStateDown(inputSource))
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
                currentPosition.y = topviewY;
                VRCamera.transform.position = currentPosition;
                CameraRig.transform.position = currentPosition;
            }
            isOnTopView = !isOnTopView;

        }
    }
}
