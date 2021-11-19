using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class GameServer : MonoBehaviourPun
{

    [SerializeField] string path = "";
    [SerializeField] GameObject playerPrefab;
    GameObject[] spawnPoints;

    Player playerServer;

    Dictionary<Player, CharacterHY> characters = new Dictionary<Player, CharacterHY>();

    private void Awake()
    {
        playerServer = PhotonNetwork.MasterClient; //Defino quién es mi servidor (El MasterClient)

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

    }

    void RunDictionary()
    {

        for (int i = 0; i < characters.Count; i++)
        {
            
        }
    }

    #region Character gameplay requests

    [PunRPC]
    public void RequestGoToWaypoint(Player client, Vector3 direction)
    {

        if (characters.ContainsKey(client))
        {
            var character = characters[client];
            if(character.NextWaypoint <= character.MaxWaypoints)
            {

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

    }

[PunRPC]
    public void RequestMovePlayer(Player client)
    {

    }
    #endregion

    #region Character set up requests

    [PunRPC]
    public void InitializePlayer(Player client)
    {
        GameObject playerInstantiatedPrefab = PhotonNetwork.Instantiate(playerPrefab.name, GetInitialPosition(), Quaternion.identity);
        CharacterHY character = playerInstantiatedPrefab.GetComponent<CharacterHY>();
        characters[client] = character;
        int characterID = character.photonView.ViewID;
        //photonView.RPC("RequestRegisterPlayer", RpcTarget.Others, client, characterID);
    }

    [PunRPC]
    public void RequestRegisterPlayer(Player client, int ID)
    {
        PhotonView characterPhotonView = PhotonView.Find(ID);
        if (characterPhotonView == null) return;

        var character = characterPhotonView.GetComponent<CharacterHY>();
        if (character == null) return;

        characters[client] = character;
    }

    [PunRPC]
    public void RequestGetPlayer(Player client)
    {
        if(characters.ContainsKey(client))
        {
            var character = characters[client];
            int characterID = character.photonView.ViewID;
            photonView.RPC("SetPlayer", client, characterID);
        }
    }

    [PunRPC]
    public void SetPlayer(int characterID)
    {
        PhotonView characterPhotonView = PhotonView.Find(characterID);
        if (characterPhotonView == null) return;

        var character = characterPhotonView.GetComponent<CharacterHY>();
        if (character == null) return;

        var controller = GameObject.FindObjectOfType<CharacterControllerHY>();
        if (controller == null) return;

        controller.setCharacter = character;
    }

    #endregion

    #region Get Info

    Vector3 GetInitialPosition()
    {

        if (spawnPoints.Length == 0) Debug.Log("NISSSSSSSSSSSSSSSMAAAAAAAAAAAAAAANNNNNNNNNNNNNNNNNNNNN");

        Vector3 spPosition = Vector3.zero;
        foreach (var sp in spawnPoints)
        {
            Debug.Log("Nisman entró en el for");
            if (sp.GetComponent<SpawnPoint>().IsAvaiable)
            {
                Debug.Log("Nisman is avaiable");
                spPosition = sp.transform.position;
                sp.GetComponent<SpawnPoint>().IsAvaiable = false;

            }
        }

        Debug.Log("SpawnPoint position" + spPosition);
        return spPosition;

    }

    #endregion

    public Player GetPlayerServer => playerServer;

}
