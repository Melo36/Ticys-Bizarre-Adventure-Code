using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
             
    }
}
