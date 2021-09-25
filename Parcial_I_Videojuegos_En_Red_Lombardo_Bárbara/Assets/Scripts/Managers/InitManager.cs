using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InitManager : MonoBehaviourPunCallbacks
{
    [SerializeField] int numberOfPlayers;

    private void Start()
    {
        var currRoom = PhotonNetwork.CurrentRoom;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.IsMasterClient)
        {

            var playersCount = PhotonNetwork.CurrentRoom.PlayerCount;

            if(playersCount >= numberOfPlayers)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
            }
        }
    }

}
