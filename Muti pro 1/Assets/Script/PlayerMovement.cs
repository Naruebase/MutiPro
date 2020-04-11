using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun,IPunObservable
{
    public float moveSpeed = 5f;

    public Rigidbody2D myRB;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    Vector3 correctPos;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.transform.position);
        }
        else if (stream.IsReading)
        {
            correctPos = (Vector3)stream.ReceiveNext();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            this.transform.position = correctPos;
        }
       
    }

    private void FixedUpdate()
    {
        myRB.MovePosition(myRB.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - myRB.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg;
        myRB.rotation = angle;
    }
}
