using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Quaternion setRotation;

    void LateUpdate()
    {
        transform.position = target.position + offset;
        transform.rotation=setRotation;
    }
}
