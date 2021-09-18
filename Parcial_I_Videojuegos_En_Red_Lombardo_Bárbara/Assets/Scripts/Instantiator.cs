using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Instantiator : MonoBehaviour
{

    public string prefabName;
    private void Start()
    {
        PhotonNetwork.Instantiate(prefabName, new Vector3(0, .5f, 0), Quaternion.identity);
    }
}
