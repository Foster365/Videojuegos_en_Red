using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    //[SerializeField] Character character;
    [SerializeField] Animator animator;

    public void MoveAnimation(string floatName, float vel)
    {
        animator.SetFloat(floatName, vel);
    }

    public void JumpAnimation()
    {
        animator.SetTrigger(CharacterAnimationTags.CHARACTER_JUMP);
    }

    public void WinAnimation()
    {
        animator.SetTrigger(CharacterAnimationTags.CHARACTER_WIN);
    }

    public void DefeatAnimation()
    {
        animator.SetTrigger(CharacterAnimationTags.CHARACTER_DEFEAT);
    }

    //TODO Add: + Victory
    //+ Defeat

}
