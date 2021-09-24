using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour //No uso Player porque Photon tiene una clase llamada Player.
{
    [SerializeField]
    float speed;
    [SerializeField]
    int initJumpForce;
    
    int score;

    int jumpForce;

    Rigidbody rbody;

    public int Score { get => score; set => score = value; }
    public int JumpForce { get => jumpForce; set => jumpForce = value; }

    public int InitJumpForce { get => initJumpForce; set => initJumpForce = value; }
    public Rigidbody Rbody { get => rbody; set => rbody = value; }

    private void Start() {
        jumpForce = initJumpForce;
    }

    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        score = 0;
    }

    public void Move(Vector3 dir)
    {
        dir *= speed;
        transform.forward = dir;
        dir.y = rbody.velocity.y;
        rbody.velocity = dir;
    }

    public bool CheckGround()
    {
        var isGrounded = Physics.Raycast(transform.position, -transform.up, 1 << LayerMask.NameToLayer(UtilitiesTags.GROUND_TAG)) ? true : false;
        return isGrounded;
    }

    public void Jump()
    {
        if (CheckGround())
            rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

}
