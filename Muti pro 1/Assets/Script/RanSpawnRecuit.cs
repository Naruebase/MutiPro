using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RanSpawnRecuit : MonoBehaviourPun
{
    public static RanSpawnRecuit instance;
    public float Speed;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InvokeRepeating("Genarate", 0, Speed);
    }

    public void Genarate()
    {
        int x = Random.Range(0, Camera.main.pixelWidth);
        int y = Random.Range(0, Camera.main.pixelHeight);

        Vector3 Target = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
        Target.z = 0;

        var num = Random.Range(1, 5);

        if(num == 1)
        {
            PhotonNetwork.Instantiate("Recuit01", Target, Quaternion.identity);
        }
        else if (num == 2)
        {
            PhotonNetwork.Instantiate("Recuit02", Target, Quaternion.identity);
        }
        else if (num == 3)
        {
            PhotonNetwork.Instantiate("Recuit03", Target, Quaternion.identity);
        }
        else if (num == 4)
        {
            PhotonNetwork.Instantiate("Recuit04", Target, Quaternion.identity);
        }

    }
}
