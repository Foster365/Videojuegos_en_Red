//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManager : MonoBehaviour
{
    //PowerUps Abstract Factory
    //PortalGunSpawner portalGunSpawner;
    Transform _transform;
    //

    [SerializeField] GameObject[] prefabs;
    float spawn = 1.5f;
    float timer = 0;
    int maxPortalGunsQuantity = 40;
    //

    //Portal Gun Prototype
    int maxPortalGuns = 50;
    [SerializeField] GameObject portalGunPrefab;
    PortalGun portalGun;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {

        timer += Time.deltaTime;//Update, si se cumple la condicion entra al if
        if (timer >= spawn)CreatePortalGun();
        //CreateHealingPoints();

    }

    #region Power_Ups_Factory
    //void CreatePortalGun()
    //{

    //    foreach (var p in prefabs)
    //    {

    //        Randrnd = Int32.Parse(Random.Range(-30, 30));
    //        //Debug.Log(timer);
    //        //Debug.Log("Current pos" + currPos);
    //        if (maxPortalGunsQuantity >= 0/* && Physics.OverlapSphere(transform.position, 3) == null */)
    //        {

    //            portalGunSpawner.CreateEnemy(p);

    //            p.transform.position += Random.insideUnitSphere * 5f;
    //            currentPosition = 0;
    //            timer = 0;
    //            maxPortalGunsQuantity--;
    //        }

    //    }
        

    //}

    #endregion

    #region Portal_Gun_Prototype
    void CreatePortalGun()
    {
        var randomPos = new Vector3(Random.Range(-30, 30), 47, 2f);
        //Vector3 initPosition = transform.position;
        //Vector3 currPosition = initPosition;
        if (maxPortalGunsQuantity >= 0)
        {
            PhotonNetwork.Instantiate("Portal_Gun_B", randomPos, transform.rotation);
            //GameObject.Destroy(portalGunPrefab, 3f);
            //healthBoxPrefab.transform.position *= 5f;
            maxPortalGunsQuantity--;
            timer = 0;
        }
    }
    #endregion
}
