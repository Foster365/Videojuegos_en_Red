using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class NetManager : MonoBehaviourPunCallbacks
{

    [SerializeField] TMP_InputField playerNickname;
    [SerializeField] Button startGameButton;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Me conecto al servidor de Photon
        startGameButton.interactable = false;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinLobby(); //Busco un lobby por default
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to lobby");
        startGameButton.interactable = true;
    }

    public void Connect()
    {

        startGameButton.interactable = true;

        PhotonNetwork.NickName = playerNickname.text;

        //if (string.IsNullOrEmpty(playerName.text) || string.IsNullOrWhiteSpace(playerName.text))
        //    PhotonNetwork.NickName = "Salame que no se puso el nombre";
        ////Hacer que vuelva a tipear, mostrar en pantalla que el nombre está vacío
        //else PhotonNetwork.NickName = playerName.text;

        //PhotonNetwork.NickName = playerName.text;

        RoomOptions options = new RoomOptions(); // Seteo las configs de la room
        //options.IsOpen = true;
        //options.IsVisible = true;
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("Lobby", options, TypedLobby.Default); // Me conecto a la sala con el nombre "BoardGameRoom" o
                                                                                      // creo una, utilizando esos settings

    }

    public override void OnJoinedRoom()
    {
        //Una vez creada la room cargo el nivel
        PhotonNetwork.LoadLevel("Lobby"); //Antes era Gameplay
    }
}
