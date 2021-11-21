using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class GameServer : MonoBehaviourPun
{

    [SerializeField] string path = "";
    //[SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject[] spawnPoints;

    Player playerServer;

    [SerializeField] Dice dice;

    Dictionary<Player, CharacterHY> characters = new Dictionary<Player, CharacterHY>();

    private void Awake()
    {
        playerServer = PhotonNetwork.MasterClient; //Defino quién es mi servidor (El MasterClient)

        //spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

    }

    void RunDictionary()
    {

        for (int i = 0; i < characters.Count; i++)
        {
            
        }
    }

    #region Character gameplay requests

    [PunRPC]
    public void SetPlayerTurn(Player client)
    {
        if(characters.ContainsKey(client))
        {
            var character = characters[client];
            if (!character.IsMyTurn) character.IsMyTurn = true;
        }
    }


    [PunRPC]
    public int ThrowDice()
    {
        int diceThrow = Random.Range(1, 6);
        //dice.RouletteAction();
        Debug.Log("Dice Throw: " + diceThrow);
        return diceThrow;
    }

    [PunRPC]
    public void RequestGoToWaypoint(Player client, int maxWp)
    {

        if (characters.ContainsKey(client))
        {
            var character = characters[client];
            if(character.NextWaypoint <= maxWp)
            {

                Debug.Log("Entró al seteo de waypoints");
                Debug.Log("Next Waypoint" + character.NextWaypoint);
                var waypoint = character.Waypoints[character.NextWaypoint];
                var waypointPosition = waypoint.transform.position;
                waypointPosition.y = transform.position.y;

                Vector3 dir = waypointPosition - transform.position;

                if (dir.magnitude < character.Distance)
                {

                    if (character.NextWaypoint + character.IndexModifier <= character.Waypoints.Count
                        || character.NextWaypoint + character.IndexModifier > 0) character.IndexModifier *= 1;

                    character.NextWaypoint += character.IndexModifier;

                }

                photonView.RPC("RequestMovePlayer", RpcTarget.MasterClient, client, dir.normalized, character.GetComponent<Rigidbody>(), 10);
                //return dir;

            }
        }

    }

[PunRPC]
    public void RequestMovePlayer(Player client, Vector3 dir, Rigidbody playerRigidbody, int speed)
    {
        dir = dir.normalized;
        var ySpeed = playerRigidbody.velocity.y;
        playerRigidbody.velocity = new Vector3(dir.x * speed, ySpeed);
    }
    #endregion

    #region Character set up requests

    [PunRPC]
    public void InitializePlayer(Player client, Vector3 position)
    {
        GameObject playerInstantiatedPrefab = PhotonNetwork.Instantiate("Cube", position, Quaternion.identity);
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

    //Vector3 GetInitialPosition()
    //{

    //    //if (spawnPoints.Length == 0) Debug.Log("NISSSSSSSSSSSSSSSMAAAAAAAAAAAAAAANNNNNNNNNNNNNNNNNNNNN");

    //    for (int i = 0; i < spawnPoints.Length; i++)
    //    {
    //        if (!spawnPoints[i].GetComponent<SpawnPoint>().IsAvaiable) return;
    //        if (spawnPoints[i].GetComponent<SpawnPoint>().IsAvaiable)
    //        {
    //            Vector3 spPosition = spawnPoints[i].transform.position;
    //            spawnPoints[i].GetComponent<SpawnPoint>().IsAvaiable = false;
    //            Debug.Log("Spawn Point Position: " + spPosition);
    //        }

    //    }

    //    return Vector3.zero;
    //    //foreach (var sp in spawnPoints)
    //    //{
    //    //Debug.Log("Nisman entró en el for");
    //    //if (sp.GetComponent<SpawnPoint>().IsAvaiable)
    //    //{
    //    //    Debug.Log("Nisman is avaiable");
    //    //    sp.GetComponent<SpawnPoint>().IsAvaiable = false;

    //    //}
    //    //}

    //}

    #endregion

    public Player GetPlayerServer => playerServer;

    public Dictionary<Player, CharacterHY> Characters { get => characters; set => characters = value; }
    public GameObject[] SpawnPoints { get => spawnPoints; set => spawnPoints = value; }
}
