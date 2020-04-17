using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Fire : MonoBehaviourPun
{
    public static Fire instance; 
    public Transform muzzle;
    public Camera cam;
    public bool canControl;
    public bool canFind = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canControl == false)
            return;

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

    public void FindTransform()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        Debug.Log("Find");
    } 

    void Shooting()
    {
        PhotonNetwork.Instantiate("Bullet Test", muzzle.position, muzzle.rotation);

        Debug.Log("Shoot");
    }
}

