using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        dir.x *= speed;
        dir.y = rbody.velocity.y;
        rbody.velocity = dir;
    }

    public void Jump()
    {
        rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

}
