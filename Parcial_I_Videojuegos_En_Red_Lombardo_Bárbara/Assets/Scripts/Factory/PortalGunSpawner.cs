using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGunSpawner : Spawner
{

    public GameObject CreateEnemy(GameObject portalGunPrefab)
    {

        GameObject portalGun = Create(portalGunPrefab).GetComponent<GameObject>();
        Debug.Log("Portal Gun Instance created");

        return portalGun;
    }
}
