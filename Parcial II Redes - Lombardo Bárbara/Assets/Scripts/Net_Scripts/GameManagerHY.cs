using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class GameManagerHY : MonoBehaviourPun
{

    Frag_Grenade grenade;
    Player client;

    private void Start()
    {
        client = PhotonNetwork.LocalPlayer;
        //grenade = GameObject.FindWithTag("Grenade_Win_Trigger").GetComponent<Frag_Grenade>();
    }

    public void Win()
    {
        if (grenade.IsWin) Win(client);
    }

    void Win(Player client)
    {
        photonView.RPC("PlayerWinCallback", RpcTarget.All, client);
    }

    [PunRPC]
    public void PlayerWinCallback(Player client)
    {
        if (PhotonNetwork.LocalPlayer == client)
        {
            PhotonNetwork.LoadLevel("Win_Screen");
        }
    }

    [PunRPC]
    public void PlayerGameOverCallback(Player client)
    {
        if(PhotonNetwork.LocalPlayer == client)
        {
            PhotonNetwork.LoadLevel("Game_Over_Screen");
        }
    }

}
