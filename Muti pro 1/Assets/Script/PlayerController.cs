using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    public float moveSpeed = 5f;

    public Rigidbody2D myRB;
    Vector2 movement;

    Vector3 correctPos;

    public string playerName;
    public float hp = 100f;
    public TextMesh playerNameText;
    public TextMesh playerHpText;

    public bool canControl;

    // Start is called before the first frame update
    void Start()
    {
        myRB = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl == false)
            return;
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement = moveInput.normalized * moveSpeed;
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

        if (hp <= 0)
        {
            hp = 0;
        }

        playerHpText.text = hp.ToString("0");
    }
}
