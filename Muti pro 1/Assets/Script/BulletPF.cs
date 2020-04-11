using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletPF : MonoBehaviourPun
{
    public float speed = 20f;

    private float countTime;

    void Update()
    {
        this.transform.Translate(Vector2.right * speed * Time.deltaTime);

        countTime += Time.deltaTime;

        if (photonView.IsMine)
        {
            if (countTime >= 3)
            {
                PhotonNetwork.Destroy(photonView);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            Destroy(this.gameObject);
        }
    }
}

