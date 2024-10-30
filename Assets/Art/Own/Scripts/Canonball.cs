using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canonball : MonoBehaviour
{
    private float x, y, z;
    private void Start()
    {
        SetPosition();
    }
    private void Update()
    {
        float amtToMove = Random.Range(1f, 40f) * Time.deltaTime;
        transform.Translate(Vector3.forward * amtToMove, Space.World);
    
    if (transform.position.z >= 37)
        {
            SetPosition();
        }
    }

    public void SetPosition()
    {
        x = Random.Range(44f, 80f);
        y = 3.3f;
        z = 8f;
        transform.position = new Vector3(x, y, z);
    }
}
