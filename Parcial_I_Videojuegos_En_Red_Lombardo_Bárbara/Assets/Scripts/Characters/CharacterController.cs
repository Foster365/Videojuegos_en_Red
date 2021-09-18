using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterController : MonoBehaviourPun
{

    Character player;

    private void Awake()
    {

        if(!photonView.IsMine) //Uso PhotonView para corroborar que el controller es de mi player
        {

            player.ChangeColor(Color.green);
            Destroy(this);
            return;

        }
        //else player.ChangeColor(Color.green);

        player = GetComponent<Character>();

    }

    // Update is called once per frame
    void Update()
    {

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        player.Move(dir.normalized);

        if (Input.GetKeyDown(KeyCode.Space))
            player.Jump();

    }
}
