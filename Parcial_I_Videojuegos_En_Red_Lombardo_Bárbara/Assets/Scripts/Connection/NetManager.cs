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

    [SerializeField] Button buttonConnect;
    [SerializeField] InputField inputFieldRoomName;
    //[SerializeField] InputField inputFieldNickName;
    [SerializeField] InputField inputFieldMaxPlayersNumber;
    //[SerializeField] InputField inputFieldCharacter;
    //public InputField InputFieldCharacter { get => inputFieldCharacter; set => inputFieldCharacter = value; }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        buttonConnect.interactable = false;
    }
    public override void OnConnectedToMaster()
    {
        print("Connected to master");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        print("Connected to lobby");
        buttonConnect.interactable = true;
    }
    public void Connect()
    {
        buttonConnect.interactable = false;
        


        RoomOptions options = new RoomOptions();
        options.IsOpen = true;
        options.IsVisible = true;
        byte maxPlayers;
        if (byte.TryParse(inputFieldMaxPlayersNumber.text, out maxPlayers))
        {
            options.MaxPlayers = maxPlayers;
        }
        else
        {
            options.MaxPlayers = 2;
        }
        string roomName = "";
        if (string.IsNullOrEmpty(inputFieldRoomName.text) || string.IsNullOrWhiteSpace(inputFieldRoomName.text))
        {
            roomName = "La cueva del Marcianeke";
        }
        else
        {
            roomName = inputFieldRoomName.text;
        }
        PhotonNetwork.JoinOrCreateRoom(roomName, options, TypedLobby.Default);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        buttonConnect.interactable = true;
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        buttonConnect.interactable = true;
    }
    public override void OnJoinedRoom()
    {
        print("Connected to Room");
        PhotonNetwork.LoadLevel("Game");
    }
}
