using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterController : MonoBehaviourPun
{

    Character player;
    [SerializeField] CharacterAnimations characterAnim;

    private void Awake()
    {

        if(!photonView.IsMine)
        {

            //Destroy(this);
            return;

        }


        player = GetComponent<Character>();

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("Character score: "+ player.Score);

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {

            Vector3 dir = new Vector3(h, 0, v);
            float characterVel = player.Rbody.velocity.magnitude;
            photonView.RPC("SetCharacterVelocity", RpcTarget.All, characterVel);

            player.Move(dir.normalized);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("SetCharacterJump", RpcTarget.All);
            player.Jump();
            characterAnim.JumpAnimation();
        }

        //textPlayerScore.text = player.Score.ToString();

    }

    #region PunRPC

    [PunRPC]
    public void SetCharacterVelocity(float vel)
    {
        characterAnim.MoveAnimation(CharacterAnimationTags.CHARACTER_MOVEMENT, vel);
    }

    [PunRPC]
    public void SetCharacterJump()
    {
        characterAnim.JumpAnimation();
    }

    #endregion

}
