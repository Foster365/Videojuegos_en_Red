using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    Rigidbody _rigidbody;
    GameObject projectile;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 dir)
    {
        dir = dir.normalized;
        dir *= movementSpeed;
        dir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = dir;
    }

    public void Shoot()
    {
        GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
    }

}
