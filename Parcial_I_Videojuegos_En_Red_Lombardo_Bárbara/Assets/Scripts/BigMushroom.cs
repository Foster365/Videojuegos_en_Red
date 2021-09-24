using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMushroom : MonoBehaviour
{
    [SerializeField] int jumpForceIncreaser;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == CharacterTags.CHARACTER_TAG)
        {
            other.gameObject.GetComponent<Character>().JumpForce += jumpForceIncreaser;
            other.gameObject.GetComponent<Character>().Jump();
        }
        other.gameObject.GetComponent<Character>().JumpForce = other.gameObject.GetComponent<Character>().InitJumpForce;
    }
}
