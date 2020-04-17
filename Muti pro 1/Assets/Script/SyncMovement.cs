using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class SyncMovement : MonoBehaviourPun,IPunObservable
{
    public PlayerController playerController;
    public Fire fire;

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerController.transform.position);
            stream.SendNext(fire.transform.position);
        }
        if (stream.IsReading)
        {
            playerController.transform.position = (Vector3)stream.ReceiveNext();
            fire.transform.position = (Vector3)stream.ReceiveNext();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            playerController.canControl = true;
            fire.canControl = true;
        }
        else
        {
            playerController.canControl = false;
            fire.canControl = false;
        }
    }
}
