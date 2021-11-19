using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class CharacterControllerHY : MonoBehaviour//Está en un GObject en la escena para que cada player que entra al juego tenga su controlador, si lo pusiera en el
    //prefab tendría que aclararle a qué client le tendría que guardar
{
    [SerializeField] GameServer gameServer;
    Player localPlayer;
    Player clientServer;

    CharacterHY characterToControl;

    private void Start()
    {

        localPlayer = PhotonNetwork.LocalPlayer; //Obtengo el localPLayer para ya tenerlo
        clientServer = gameServer.GetPlayerServer;

        gameServer.photonView.RPC("InitializePlayer", clientServer, localPlayer);
        gameServer.photonView.RPC("RequestGetPlayer", clientServer, localPlayer);
    }

    private void Update()
    {
        if (characterToControl == null) return;

        //if(characterToControl.IsMyTurn && Input.GetKeyDown(KeyCode.W)) gameServer.photonView.RPC("MovePlayer", clientServer, localPlayer);

    }

    public CharacterHY setCharacter //El controller setea al character a controlar
    {
        set
        {
            characterToControl = value;
        }
    }
}
