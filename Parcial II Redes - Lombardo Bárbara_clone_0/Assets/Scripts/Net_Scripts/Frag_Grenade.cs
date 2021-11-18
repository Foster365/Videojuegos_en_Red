using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Frag_Grenade : MonoBehaviour
{

    bool isWin;

    public bool IsWin { get => isWin; set => isWin = false; }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            IsWin = true;
        }
    }

}
