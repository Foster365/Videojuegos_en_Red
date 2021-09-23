using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] Animator animator;

    public void MoveAnimation(bool isMoving)
    {
        animator.SetBool(CharacterAnimationTags.CHARACTER_MOVEMENT, isMoving);
    }

    public void JumpAnimation()
    {
        animator.SetTrigger(CharacterAnimationTags.CHARACTER_JUMP);
    }

    //TODO Add: + Victory
                //+ Defeat

}
