using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class GameServer : MonoBehaviourPun
{

    [SerializeField] string prefabsFolder = "";
    //[SerializeField] GameObject[] prefabs;
    [SerializeField] GameObject prefab;

    GameObject[] spawnPoints;

    Player gameServer;
    Dictionary<Player, CharacterHY> characters = new Dictionary<Player, CharacterHY>();

    private void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    private void Awake()
    {
        gameServer = PhotonNetwork.MasterClient;
    }

    [PunRPC]
    public void InitializePlayer(Player clientPlayer)
    {

        //GameObject playerNameToInstantiate = GetPlayerToInstantiate();

        GameObject obj = PhotonNetwork.Instantiate(prefabsFolder + "/" + prefab.name, GetInitialPosition(), Quaternion.identity);
        CharacterHY character = obj.GetComponent<CharacterHY>();
        characters[clientPlayer] = character;

        //int characterID = character.photonview.ViewID; // Tira error porque no tiene los libraries de photon.



    }

    [PunRPC]
    public void RequestRegisterPlayer(Player clientPlayer, int playerID)
    {

        PhotonView photonView = PhotonView.Find(playerID);
        if (photonView == null) return;

        var character = photonView.GetComponent<CharacterHY>();
        if (character == null) return;
        characters[clientPlayer] = character;

    }

    #region Get Info

    Vector3 GetInitialPosition()
    {
        Vector3 spPosition = new Vector3(0, 0, 0);
        foreach (var sp in spawnPoints)
        {
            if (sp.GetComponent<SpawnPoint>().IsAvaiable)
            {
                spPosition = sp.transform.position;
                break;

            }
        }

        return spPosition;

    }

    //GameObject GetPlayerToInstantiate()
    //{
    //    return Random.Range(0, prefabs.Length);
    //}

    #endregion
}
