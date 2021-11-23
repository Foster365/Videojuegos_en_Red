using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimations : MonoBehaviour
{

    Animator characterAnim;
    Dictionary<string, System.Action> animationsDictionary = new Dictionary<string, System.Action>();

    public Animator CharacterAnim { get => characterAnim; set => characterAnim = value; }

    private void Awake()
    {
        characterAnim = GetComponent<Animator>();
    }

    public void MovingAnimation(float characterMovementVel)
    {
        characterAnim.SetFloat("Character_Velocity", characterMovementVel);
    }

    //public Actions()
    //{
    //    animationsDictionary["myKey"] = MovingAnim;
    //}

    #region Character Animations
    public void MovingAnim()
    {
        //characterAnim.SetTrigger();
    }
    #endregion

}
