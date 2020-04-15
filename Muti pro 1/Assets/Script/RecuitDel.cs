using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RecuitDel : MonoBehaviourPun
{
    float count;
    public float del;

    // Update is called once per frame
    void FixedUpdate()
    {
        count += 1 * Time.deltaTime;

        if (count > 5)
        {
            count = 5;
        }
    }

    private void Update()
    {
        if (count == del)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
