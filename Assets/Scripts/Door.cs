using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Door : MonoBehaviourPunCallbacks
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
        if (other.gameObject.tag == "Player")
        {
            ConnectToServer();
        }        
    }

    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 25;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.LoadLevel("SampleScene");

        PhotonNetwork.JoinOrCreateRoom("FIU", roomOptions, TypedLobby.Default);
    }
}
