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
    [SerializeField] Transform sp;

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

        PhotonNetwork.Instantiate("Player", sp.position, Quaternion.identity);

        //GameObject playerNameToInstantiate = GetPlayerToInstantiate();

        //if(!characters[clientPlayer].gameObject.GetComponent<CharacterHY>().IsSpawn)
        //{

        //    GameObject obj = PhotonNetwork.Instantiate("Player", sp.position, Quaternion.identity);
        //    CharacterHY character = obj.GetComponent<CharacterHY>();
        //    characters[clientPlayer] = character;
        //    character.IsSpawn = true;

        //}
        //int characterID = character.photonview.ViewID; // BUG ALERT Tira error porque no tiene los libraries de photon.

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

    #region Server Requests

    [PunRPC]
    public void RequestMove(Player client, Vector3 direction)
    {
        if(characters.ContainsKey(client))
        {
            var character = characters[client];
            character.Move(direction);
        }
    }

    [PunRPC]
    void RequestGotoWaypoint(Player client) // Retornaba Vector3
    {

        if(characters.ContainsKey(client))
        {
            var character = characters[client];

            var waypoint = character.Waypoints[character.NextWaypoint];
            var waypointPosition = waypoint.transform.position;
            waypointPosition.y = transform.position.y;

            Vector3 dir = waypointPosition - transform.position;

            if (dir.magnitude < character.Distance)
            {

                if (character.NextWaypoint + character.IndexModifier >= character.Waypoints.Count
                    || character.NextWaypoint + character.IndexModifier < 0) character.IndexModifier *= -1;

                character.NextWaypoint += character.IndexModifier;

            }

            photonView.RPC("RequestMove", client, (client, dir.normalized));
            //return dir;

        }


    }

    [PunRPC]
    public void RequestAnim(Player client, string animName)
    {
        if(characters.ContainsKey(client))
        {
            var character = characters[client];

            //character.PlayAnimation(animName); //TODO FIX

        }
    }

    #endregion

    #region Get Info

    Vector3 GetInitialPosition()
    {
        Vector3 spPosition = new Vector3(0, 0, 0);
        foreach (var sp in spawnPoints)
        {
            if (sp.GetComponent<SpawnPoint>().IsAvaiable)
            {
                spPosition = sp.transform.position;
                //sp.GetComponent<SpawnPoint>().IsAvaiable = false;

            }
        }

        return spPosition;

    }

    //GameObject GetPlayerToInstantiate()
    //{
    //    return Random.Range(0, prefabs.Length);
    //}

    #endregion
    public Player GetPlayerServer => gameServer;
}
