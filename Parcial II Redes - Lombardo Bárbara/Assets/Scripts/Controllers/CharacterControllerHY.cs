using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class CharacterControllerHY : MonoBehaviour
{

    [SerializeField] GameServer gameServer;
    Player localPlayer;
    CharacterHY character;

    int squaresCount = 0;
    int diceSquaresAmount;


    private void Start()
    {

        localPlayer = PhotonNetwork.LocalPlayer;
        Player clientServer = gameServer.GetPlayerServer;

        if (gameServer == null) Debug.Log("Game Server is null");
        else if (localPlayer == null) Debug.Log("Local Player is null");
        else if (clientServer == null) Debug.Log("Client Server is null");
        gameServer.photonView.RPC("InitializePlayer", localPlayer, localPlayer);
        //PlayerInitializationServerRequests();

    }

    private void Update()
    {

        if (character == null) return;

        Vector3 dir = transform.forward;

        if (Input.GetKey(KeyCode.W)) gameServer.photonView.RPC("RequestMove", localPlayer, localPlayer, dir);
        
    }


    #region Server Requests

    void PlayerInitializationServerRequests()
    {

        gameServer.photonView.RPC("RequestGetPlayer", gameServer.GetPlayerServer, localPlayer);
        gameServer.photonView.RPC("InitializePlayer", gameServer.GetPlayerServer, localPlayer);

    }

    #endregion
}
