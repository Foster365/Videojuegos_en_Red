using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public enum TurnState { START, PLAYERTURN, WON, GAMEOVER }

public class TurnBasedSystem : MonoBehaviour
{

    [SerializeField] GameServer gameServer;
    [SerializeField] DiceController diceController;
    [SerializeField] Transform[] spawnPoints;

    TurnState currentState;

    List<Transform> spList = new List<Transform>();

    Player localPlayer;
    Player clientServer;

    void Start()
    {
        localPlayer = PhotonNetwork.LocalPlayer; //Obtengo el localPLayer para ya tenerlo
        clientServer = gameServer.GetPlayerServer;

        currentState = TurnState.START;

        spList = spawnPoints.ToList();

        //if(PhotonNetwork.IsMasterClient)
            StartCoroutine(SetUpGame());

    }

    private void Update()
    {

        SetGameTurnActions();
        //if (Input.GetKeyDown(KeyCode.T))
        //{

        //    gameServer.photonView.RPC("ThrowDice", RpcTarget.MasterClient);
        //    gameServer.photonView.RPC("RequestMovePlayer", RpcTarget.MasterClient);
        //}
    }

    IEnumerator SetUpGame()
    {

        //SetGameTurnActions();

        GetInitialPosition();

        currentState = TurnState.PLAYERTURN;

        yield return new WaitForSeconds(2f);

    }

    void SetGameTurnActions()
    {

        if(PhotonNetwork.IsMasterClient && currentState == TurnState.PLAYERTURN)
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {

                gameServer.photonView.RPC("SetPlayerTurn", clientServer, PhotonNetwork.PlayerList[i]);

                Debug.Log("Throwing Dice...");

                gameServer.photonView.RPC("ThrowDice", clientServer);
                StartCoroutine(WaitUntilMove());
                gameServer.photonView.RPC("RequestGoToWaypoint", clientServer, PhotonNetwork.PlayerList[i], 3);

            }

        }

    }

    IEnumerator WaitUntilMove()
    {
        Debug.Log("Waiting to move");
        yield return new WaitForSeconds(2f);
    }

    void GetInitialPosition()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{

            for (int i = 0; i < spList.Count; i++)
            {

                //gameServer.photonView.RPC("RequestGetPlayer", clientServer, clientServer);

                if (spList[i].GetComponent<SpawnPoint>().IsAvaiable)
                {

                    Debug.Log("Spawn points list count: " + spList.Count);

                    Vector3 spPosition = spList[i].transform.position;

                    gameServer.photonView.RPC("InitializePlayer", gameServer.GetPlayerServer, localPlayer, spPosition);
                    spList.RemoveAt(i);

                    Debug.Log("Spawn Point Position: " + spPosition);
                    Debug.Log("Spawn Points List Count: " + spList.Count);
                    break;
                }
            }
    }

}
