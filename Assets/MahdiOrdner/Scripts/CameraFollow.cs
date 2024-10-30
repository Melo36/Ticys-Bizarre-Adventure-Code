using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private Transform followedObject;

    private void LateUpdate()
    {
        transform.position = followedObject.position + Vector3.up * distanceUp - followedObject.right * distanceAway;
        transform.LookAt(followedObject);
    }
}
