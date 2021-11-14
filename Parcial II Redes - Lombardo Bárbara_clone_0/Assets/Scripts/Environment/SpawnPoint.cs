using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    bool isAvaiable;

    public bool IsAvaiable { get => isAvaiable; set => isAvaiable = true; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, .5f);

    }

}
