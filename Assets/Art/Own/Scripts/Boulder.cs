using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private float x, y, z;
    public float boulderSpeed;
  
    private void Update()
    {
        float amtToMove = boulderSpeed * Time.deltaTime;
        transform.Translate(Vector3.left * amtToMove, Space.World);
        
    }

    public void SetPosition()
    {
        x = -5481;
        y = 34.36f;
        z = 181.62f;
        transform.position = new Vector3(x, y, z);
    }
    
}
