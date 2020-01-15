using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;

public class UserManager : MonoBehaviourPunCallbacks
{
    public static GameObject UserMeInstance;

    public Material PlayerLocalMat;
    /// <summary>
    /// Represents the GameObject on which to change the color for the local player
    /// </summary>
    public GameObject GameObjectLocalPlayerColor;

    /// <summary>
    /// The FreeLookCameraRig GameObject to configure for the UserMe
    /// </summary>
    GameObject goFreeLookCameraRig = null;

    private bool isOnTopView = false;

    void Awake()
    {
        if (photonView.IsMine)
        {
            Debug.LogFormat("Avatar UserMe created for userId {0}", photonView.ViewID);
            UserMeInstance = gameObject;
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        if(Input.GetButtonDown("TopView"))
        {
            if(isOnTopView)
            {
                Vector3 currentPosition = UserMeInstance.transform.position;
                currentPosition.y = 1f;
                UserMeInstance.gameObject.transform.SetPositionAndRotation(currentPosition, UserMeInstance.transform.rotation);
            }
            else
            {
                Vector3 currentPosition = UserMeInstance.transform.position;
                currentPosition.y = GameObject.Find("TopView").gameObject.transform.position.y + 1f;
                UserMeInstance.gameObject.transform.SetPositionAndRotation(currentPosition, UserMeInstance.transform.rotation);
            }
            isOnTopView = !isOnTopView;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("isLocalPlayer:" + photonView.IsMine);
        updateGoFreeLookCameraRig();
        followLocalPlayer();
        activateLocalPlayer();
    }

    [PunRPC]
    public void ActivateDeactivateFire()
    {
        
    }

    /// <summary>
    /// Get the GameObject of the CameraRig
    /// </summary>
    protected void updateGoFreeLookCameraRig()
    {
        if (!photonView.IsMine) return;
        try
        {
            // Get the Camera to set as the followed camera
            goFreeLookCameraRig = transform.Find("/FreeLookCameraRig").gameObject;
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("Warning, no goFreeLookCameraRig found\n" + ex);
        }
    }

    /// <summary>
    /// Make the CameraRig following the LocalPlayer only.
    /// </summary>
    protected void followLocalPlayer()
    {
        if (photonView.IsMine)
        {
            if (goFreeLookCameraRig != null)
            {
                // find Avatar EthanHips
                Transform transformFollow = transform.Find("EthanSkeleton/EthanHips") != null ? transform.Find("EthanSkeleton/EthanHips") : transform;
                // call the SetTarget on the FreeLookCam attached to the FreeLookCameraRig
                goFreeLookCameraRig.GetComponent<FreeLookCam>().SetTarget(transformFollow);
                Debug.Log("ThirdPersonControllerMultiuser follow:" + transformFollow);

                //Lock the cursor at the middle of the screen and make it disappear

            }
        }
    }

    protected void activateLocalPlayer()
    {
        // enable the ThirdPersonUserControl if it is a Loacl player = UserMe
        // disable the ThirdPersonUserControl if it is not a Loacl player = UserOther
        GetComponent<ThirdPersonUserControl>().enabled = photonView.IsMine;
        GetComponent<Rigidbody>().isKinematic = !photonView.IsMine;
        if (photonView.IsMine)
        {
            try
            {
                // Change the material of the Ethan Glasses
                GameObjectLocalPlayerColor.GetComponent<Renderer>().material = PlayerLocalMat;
            }
            catch (System.Exception)
            {

            }
        }
    }
}

