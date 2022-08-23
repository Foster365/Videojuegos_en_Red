using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NetManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    Button buttonConnect;
    [SerializeField]
    InputField inputFieldRoom;
    [SerializeField]
    InputField inputFieldNickName;
    [SerializeField]
    InputField inputFieldPlayersNumber;

    //List<string> nickNames;

    private void Start()
    {

        buttonConnect.interactable = false;
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster() //1
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() //2
    {
        print("Connected to lobby");
        buttonConnect.interactable = true;
    }


    public void Connect()
    {

        if (string.IsNullOrWhiteSpace(inputFieldRoom.text) || string.IsNullOrEmpty(inputFieldRoom.text)) return;
        if (string.IsNullOrWhiteSpace(inputFieldNickName.text) || string.IsNullOrEmpty(inputFieldNickName.text)) return;

        PhotonNetwork.NickName = inputFieldNickName.text;

        RoomOptions options = new RoomOptions();
        options.IsOpen = true;
        options.IsVisible = true;

        byte maxPlayers;

        if (byte.TryParse(inputFieldRoom.text, out maxPlayers)) options.MaxPlayers = maxPlayers;
        else options.MaxPlayers = 10;

        string roomName = "";
        if (string.IsNullOrEmpty(inputFieldRoom.text) || string.IsNullOrWhiteSpace(inputFieldRoom.text))
        {
            roomName = "La Cueva del Marcianeke";
        }
        else
        {
            roomName = inputFieldRoom.text;
        }
        PhotonNetwork.JoinOrCreateRoom(roomName, options, TypedLobby.Default);

        buttonConnect.interactable = false;

    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game"); //Usar esta función y no la de unity, porque la carga de manera local
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        buttonConnect.interactable = true;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        buttonConnect.interactable = true;
    }

}
