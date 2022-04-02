using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Door : MonoBehaviourPunCallbacks
{
    public string sceneName;

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
        if (other.gameObject.tag == "Player")
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LeaveRoom();
                EnterScene();
            }
            else
            {
                bool isConnected = PhotonNetwork.ConnectUsingSettings();
                if (isConnected)
                {
                    EnterScene();
                }
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public void EnterScene()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 25;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.LoadLevel(sceneName);

        PhotonNetwork.JoinOrCreateRoom(sceneName, roomOptions, TypedLobby.Default);
    }
}
