using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    [SerializeField] TMP_Text playerCountText;

    private void Update()
    {
        playerCountText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();

    }


    [PunRPC]
    void LoadLevel()
    {
        PhotonNetwork.LoadLevel("Gameplay");
    }

    public override void OnPlayerEnteredRoom(Player newPLayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            if (playerCount == 4)
                photonView.RPC("LoadLevel", RpcTarget.All);
        }

    }

    IEnumerator WaitToLoadLevel()
    {
        yield return new WaitForSeconds(5f);
    }

}
