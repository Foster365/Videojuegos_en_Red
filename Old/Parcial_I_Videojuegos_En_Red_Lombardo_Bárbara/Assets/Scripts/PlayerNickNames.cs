using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerNickNames : MonoBehaviour
{

    public Text text;

    // Update is called once per frame
    void Update()
    {

        Player[] players = PhotonNetwork.PlayerList;
        text.text = "Players:" + "\n"; // \n === <br></br>

        for (int i = 0; i < players.Length; i++)
        {

            var curr = players[i];
            text.text += curr.NickName + "\n";

        }

    }
}
