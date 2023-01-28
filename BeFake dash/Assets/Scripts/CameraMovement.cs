using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(0, 10)]
    public float smoothFactor;

    void FixedUpdate()
    {
        Vector3 movePosition = target.position + offset;
        Vector3 smooth = Vector3.Lerp(transform.position, movePosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smooth;
    }
}
