using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class DiceController : MonoBehaviourPun
{

    Dice dice;

    private void Awake()
    {
        dice = GetComponent<Dice>();
    }

    //[PunRPC]
    //public void ThrowDice()
    //{
    //    dice.RouletteAction();
    //}

}
