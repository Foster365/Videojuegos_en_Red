using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimations : MonoBehaviour
{

    Animator characterAnim;
    Dictionary<string, System.Action> animationsDictionary = new Dictionary<string, System.Action>();

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
