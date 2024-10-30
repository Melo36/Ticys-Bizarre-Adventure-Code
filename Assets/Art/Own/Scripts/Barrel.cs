using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private float x, y, z;
   
    private void Update()
    {
       
        if (transform.position.y <= -5)
        {
            SetPosition();
        }
    }

    public void SetPosition()
    {
        x = 89f;
        y = 19.20f;
        z = 100f;
        transform.position = new Vector3(x, y, z);
    }
}

