using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BigMushroom : MonoBehaviourPun
{
    [SerializeField] int jumpForceIncreaser;
    [SerializeField] MushroomAnimations mushroomAnimations;

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == CharacterTags.CHARACTER_TAG)
        {
            other.gameObject.GetComponent<Character>().JumpForce += jumpForceIncreaser;
            photonView.RPC("PlayerJump", RpcTarget.All, other.gameObject);
            photonView.RPC("GetHit", RpcTarget.All);
            return;
        }
        other.gameObject.GetComponent<Character>().JumpForce = other.gameObject.GetComponent<Character>().InitJumpForce;
    }

    [PunRPC]
    public void PlayerJump(GameObject character)
    {
        character.GetComponent<Character>().Jump();
    }

    [PunRPC]
    public void GetHit()
    {
        mushroomAnimations.MushroomJumpCollision();
    }
}
