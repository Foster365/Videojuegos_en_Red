using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour //No uso Player porque Photon tiene una clase llamada Player.
{
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpForce;
    int score;

    Rigidbody rbody;

    public int Score { get => score; set => score = value; }

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
