using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconFollow : MonoBehaviour
{
    [SerializeField]
    private Transform follow;

    void Update()
    {
        Vector3 player = follow.position;
        float x = player.x / 1f;
        float z = player.z / 1f;
        transform.position = new Vector3(x, 20, z);
    }
}
