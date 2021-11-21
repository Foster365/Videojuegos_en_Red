using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    bool isAvaiable = true;

    public bool IsAvaiable { get => isAvaiable; set => isAvaiable = value; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, .1f);

    }

}
