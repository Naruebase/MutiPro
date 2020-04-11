using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Fire : MonoBehaviourPun
{
    public Transform muzzle;


    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shooting", 0f, 0.3f);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shooting");
            }
        }
    }

    void Shooting()
    {
        PhotonNetwork.Instantiate("Bullet Test", muzzle.position, muzzle.rotation);

        Debug.Log("Shoot");
    }
}

