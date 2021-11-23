using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothSpeed = .01f;
    [SerializeField] Vector3 offset;

    void FixedUpdate()
    {

        transform.rotation = target.rotation;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); //Si quiero hacer el movimiento mas suave
        transform.position = smoothedPosition;

        transform.LookAt(target);

    }
}
