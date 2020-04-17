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
    public GameObject ObjCamera;
    public CameraFollow cameraFollow;
    public Fire fire;

    private List<string> roomNameList = new List<string>();

    public enum RoomState
    {
        None,
        Connected,
        JoinedLobby,
        JoinedRoom,
        RoleCreate,
        RoleJoin,
    }

    public RoomState roomState;
    public string inputRoomName;
    public string inputPlayName;

    private void OnGUI()
    {
        switch (roomState)
        {
            case RoomState.JoinedLobby:
            {
                inputPlayName = GUILayout.TextField(inputPlayName);

                if(GUILayout.Button("Create Room"))
                {
                    roomState = RoomState.RoleCreate;
                }
                if(GUILayout.Button("Join Room"))
                {
                    roomState = RoomState.RoleJoin;
                }
                break;
            }
            case RoomState.RoleCreate:
            {
                    inputRoomName = GUILayout.TextField(inputRoomName);

                    if (GUILayout.Button("Create"))
                    {
                        PhotonNetwork.CreateRoom(inputRoomName);
                    }
                    break;
            }
            case RoomState.RoleJoin:
            {
                foreach(var roomName in roomNameList)
                    {
                        if (GUILayout.Button(roomName))
                        {
                            PhotonNetwork.JoinRoom(roomName);
                        }
                    }
                break;
            }
            case RoomState.JoinedRoom:
            {
                GUILayout.TextArea(PhotonNetwork.CurrentRoom.Name);
                GUILayout.TextArea(PhotonNetwork.GetPing().ToString());
                GUILayout.TextArea(PhotonNetwork.CurrentRoom.PlayerCount.ToString());

                if (GUILayout.Button("Exit"))
                {
                    roomState = RoomState.JoinedLobby;
                    PhotonNetwork.LeaveRoom();
                }

                break;
            }
            default:
            {
                if(GUILayout.Button("Connect to Server"))
                {
                    StartCoroutine(Connect());
                }
                break;
            }

        }
    }


    private void Start()
    {
        //StartCoroutine(Connect());
    }

    IEnumerator Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        
        while(PhotonNetwork.NetworkClientState != ClientState.ConnectedToMasterServer)
        {
            yield return null;
        }

        roomState = RoomState.Connected;

        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("OnJoinedLobby");

        roomState = RoomState.JoinedLobby;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("OnjoinedRoom");

        roomState = RoomState.JoinedRoom;

        myCharacter = PhotonNetwork.Instantiate(characterPrefName, spawnPoint.position, spawnPoint.rotation);

        Parent(myCharacter, ObjCamera);

        cameraFollow.FindTransform();

        fire = FindObjectOfType<Fire>();

        fire.FindTransform();

        var myCharacterMove = myCharacter.GetComponent<PlayerController>();
        myCharacterMove.SetPlayerName(inputPlayName);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        roomNameList.Clear();

        for (int i = 0; i < roomList.Count; i++)
        {
            roomNameList.Add(roomList[i].Name);
        }
    }

    void Parent(GameObject parentOb, GameObject childOb)
    {
        childOb.transform.parent = parentOb.transform;
    }
}
