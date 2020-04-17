using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    bool canFind = true;
    public GameObject player;
    public Transform playerTransform;
    public float speed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);

            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX,clampedY), speed);
        }
    }

    public void FindTransform()
    {
        if (canFind == true)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerTransform = player.transform;
            transform.position = playerTransform.position;
            canFind = false;
        } 
       
    }
}
