using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviour
{
    // Active Avatar Game Objects
    public GameObject activeHead;
    public GameObject activeFace;
    public GameObject activeHairStyle;

    // GameObjects and Materials for customization
    public GameObject[] heads;
    public Material[] skinTones;
    public GameObject[] facesMale;
    public GameObject[] facesFemale;
    public GameObject[] hairStylesMale;
    public GameObject[] hairStylesFemale;
    public Material[] hairColors;

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private PhotonView photonView;
    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        XROrigin rig = FindObjectOfType<XROrigin>();
        headRig = rig.transform.Find("Camera Offset/Main Camera");
        leftHandRig = rig.transform.Find("Camera Offset/LeftHand Controller");
        rightHandRig = rig.transform.Find("Camera Offset/RightHand Controller");

        if (photonView.IsMine)
        {
            gameObject.tag = "User";
            UpdateAvatar();
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);
        }
    }

    void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }

    public void UpdateAvatar()
    {
        int headSelection = PlayerPrefs.GetInt(AvatarTypes.HEAD, 0);
        int skinToneSelection = PlayerPrefs.GetInt(AvatarTypes.SKIN, 0);
        int faceSelection = PlayerPrefs.GetInt(AvatarTypes.FACE, 0);
        int hairStyleSelection = PlayerPrefs.GetInt(AvatarTypes.HAIR, 0);
        int hairColorSelection = PlayerPrefs.GetInt(AvatarTypes.HAIR_COLOR, 0);

        UpdateAvatarRPC(headSelection, skinToneSelection, faceSelection, hairStyleSelection, hairColorSelection);
    }

    [PunRPC]
    public void UpdateAvatarRPC(int headSelection, int skinToneSelection, int faceSelection, int hairStyleSelection, int hairColorSelection)
    {
        if (activeHead) Destroy(activeHead);
        activeHead = Instantiate(heads[headSelection], new Vector3(0, -1.5f, 0), head.rotation, head.transform);
        activeHead.GetComponent<Renderer>().material = skinTones[skinToneSelection];
        if (activeFace) Destroy(activeFace);
        if (headSelection == 0)
        {
            activeFace = Instantiate(facesMale[faceSelection * hairColors.Length + hairColorSelection], new Vector3(0, -1.5f, 0), head.rotation, head.transform);
        }
        else
        {
            activeFace = Instantiate(facesFemale[faceSelection * hairColors.Length + hairColorSelection], new Vector3(0, -1.5f, 0), head.rotation, head.transform);
        }
        if (activeHairStyle) Destroy(activeHairStyle);
        if (headSelection == 0)
        {
            activeHairStyle = Instantiate(hairStylesMale[hairStyleSelection], new Vector3(0, -1.5f, 0), head.rotation, head.transform);
        }
        else
        {
            activeHairStyle = Instantiate(hairStylesFemale[hairStyleSelection], new Vector3(0, -1.5f, 0), head.rotation, head.transform);
        }
        activeHairStyle.GetComponent<Renderer>().material = hairColors[hairColorSelection];

        if (photonView.IsMine) photonView.RPC("UpdateAvatarRPC", RpcTarget.OthersBuffered, headSelection, skinToneSelection, faceSelection, hairStyleSelection, hairColorSelection);
    }
}
