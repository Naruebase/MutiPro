using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D myRB;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        myRB = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement = moveInput.normalized * moveSpeed;
    }

    private void FixedUpdate()
    {
        myRB.MovePosition(myRB.position + movement * Time.fixedDeltaTime);
    }
}
