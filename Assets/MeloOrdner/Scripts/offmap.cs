using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offmap : MonoBehaviour
{
    Vector3 p;
    private void Start()
    {
        p = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="DeathZone")
        {
            transform.position = new Vector3(p.x, p.y + 200, p.z);
        }
    }
}
