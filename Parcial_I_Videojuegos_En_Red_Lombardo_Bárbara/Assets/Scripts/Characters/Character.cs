using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour //No uso Player porque Photon tiene una clase llamada Player.
{
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpForce;

    Rigidbody rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();
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
