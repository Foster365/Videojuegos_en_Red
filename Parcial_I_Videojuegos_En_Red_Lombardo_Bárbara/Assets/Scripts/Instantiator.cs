using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Instantiator : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] string prefabName;
    GameObject characterPrefab;
    //[SerializeField] Text[] textCharactersScores;

    public GameObject CharacterPrefab { get => characterPrefab; set => characterPrefab = value; }

    //[SerializeField] NetManager netMgr;
    private void Start()
    {
        InstantiatePrefabs();

    }

    //void InstantiateSpawnPoints()
    //{
    //    foreach (SpawnPoint sp in spawnPoints)
    //    {
    //        PhotonNetwork.Instantiate(sp.transform.position, Quaternion.identity);
    //    }
    //}

    //private void Update()
    //{
    //    CheckScoreValue();
    //}

    void InstantiatePrefabs()
    {
        characterPrefab = PhotonNetwork.Instantiate("Jammo_Player", spawnPoint.transform.position, Quaternion.identity);
            //return;
            //var nickName = PhotonNetwork.Instantiate("NickNamePrefab", point.position, point.rotation);
            //nickName.GetComponent<NickName>().SetNick(PhotonNetwork.LocalPlayer.NickName, obj);
            //else return;

    }

}
