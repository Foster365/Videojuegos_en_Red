using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] TriggerTile triggerTileAssociated;
    [SerializeField] GameObject turretCannon;



    private void Update()
    {

        if (triggerTileAssociated.IsCollision)
        {

            Vector3.RotateTowards(turretCannon.transform.position, triggerTileAssociated.Target.transform.position, 1, 1);
            Shoot();

        }

    }

    void Shoot()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3))
        {
            CharacterHY characterTarget = hit.collider.gameObject.GetComponent<CharacterHY>();

            if (characterTarget != null) triggerTileAssociated.GetComponent<Respawner>().Respawn(characterTarget.transform);

        }
            //triggerTileAssociated.Target.LifeController.Respawn();
            triggerTileAssociated.gameObject.GetComponent<Respawner>().Respawn(triggerTileAssociated.Target.transform);


    }

}
