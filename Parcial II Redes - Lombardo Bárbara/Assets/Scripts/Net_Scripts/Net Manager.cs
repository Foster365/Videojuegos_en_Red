using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class NetManager : MonoBehaviourPunCallbacks
{

    [SerializeField] InputField playerName;
    [SerializeField] Button startGameButton;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        startGameButton.interactable = false;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        startGameButton.interactable = false;

        if (string.IsNullOrEmpty(playerName.text) || string.IsNullOrWhiteSpace(playerName.text))
            PhotonNetwork.NickName = "Salame que no se puso el nombre";
        //Hacer que vuelva a tipear, mostrar en pantalla que el nombre está vacío
        else PhotonNetwork.NickName = playerName.text;

        //PhotonNetwork.NickName = playerName.text;

        RoomOptions options = new RoomOptions();
        options.IsOpen = true;
        options.IsVisible = true;
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("BoardGameRoom", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Gameplay");
    }
}
