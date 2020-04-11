using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    public string characterPrefName;
    public Transform spawnPoint;

    private GameObject myCharacter;

    private void Start()
    {
        StartCoroutine(Connect());
    }

    IEnumerator Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        
        while(PhotonNetwork.NetworkClientState != ClientState.ConnectedToMasterServer)
        {
            yield return null;
        }

        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("OnJoinedLobby");

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom("TEST", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("OnjoinedRoom");

        myCharacter = PhotonNetwork.Instantiate(characterPrefName, spawnPoint.position, spawnPoint.rotation);
    }
}
