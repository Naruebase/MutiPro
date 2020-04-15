using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun,IPunObservable
{
    public float moveSpeed = 5f;

    public Rigidbody2D myRB;

    public string playerName;
    public float hp = 100f;
    public TextMesh playerNameText;

    Vector2 movement;
    Vector2 mousePos;

    Vector3 correctPos;

    private void Start()
    {
        myRB = this.GetComponent<Rigidbody2D>();
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.transform.position);
            stream.SendNext(playerName);
        }
        else if (stream.IsReading)
        {
            correctPos = (Vector3)stream.ReceiveNext();
            playerNameText.text = (string)stream.ReceiveNext();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            movement = moveInput.normalized * moveSpeed;
        }
        else
        {
            this.transform.position = correctPos;
        }
       
    }

    private void FixedUpdate()
    {
        myRB.MovePosition(myRB.position + movement * Time.fixedDeltaTime);
    }

    public void SetPlayerName(string newName)
    {
        if (photonView.IsMine)
        {
            playerName = newName;
            playerNameText.text = playerName;
        }
    }

    public void TakeDamage(float damage)
    {
        photonView.RPC("RPCTakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    public void RPTakeDamage(float damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            hp = 0;
        }
    }
}
