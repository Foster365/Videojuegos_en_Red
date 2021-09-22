using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Instantiator : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] string prefabName;

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

    void InstantiatePrefabs()
    {

            PhotonNetwork.Instantiate("ElSonic", spawnPoint.transform.position, Quaternion.identity);
            //return;
            //var nickName = PhotonNetwork.Instantiate("NickNamePrefab", point.position, point.rotation);
            //nickName.GetComponent<NickName>().SetNick(PhotonNetwork.LocalPlayer.NickName, obj);
            //else return;

    }
}
