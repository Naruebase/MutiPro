using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Fire : MonoBehaviourPun
{
    public Transform muzzle;

    public Camera cam;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            Vector2 dir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;

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

