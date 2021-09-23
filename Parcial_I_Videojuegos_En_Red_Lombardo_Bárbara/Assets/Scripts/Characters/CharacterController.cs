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

        if(!photonView.IsMine) //Uso PhotonView para corroborar que el controller es de mi player
        {

            //player.ChangeColor(Color.green);
            Destroy(this);
            return;

        }
        //else player.ChangeColor(Color.green);

        player = GetComponent<Character>();

    }

    // Update is called once per frame
    void Update()
    {

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {

            Vector3 dir = new Vector3(h, 0, v);
            characterAnim.MoveAnimation(true);
            player.Move(dir.normalized);

        }
        else characterAnim.MoveAnimation(false);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {

            player.Jump();
            characterAnim.JumpAnimation();
        }

    }
}
