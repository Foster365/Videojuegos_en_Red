using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{

    [SerializeField] RespawnPoint respawnPoint;
    //[SerializeField] GameObject target;


    public void Respawn(Transform target)
    {
        target.position = respawnPoint.transform.position;
    }

}
