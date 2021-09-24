using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAnimations : MonoBehaviour
{

    Animator mushroomAnimator;

    private void Awake()
    {
        mushroomAnimator = GetComponent<Animator>();
    }

    public void MushroomJumpCollision()
    {
        mushroomAnimator.SetTrigger(EntityAnimationTags.MUSHROOM_JUMP_COLLISION);
    }

}
