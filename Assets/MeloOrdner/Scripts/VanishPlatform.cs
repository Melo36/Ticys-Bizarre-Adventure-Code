using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishPlatform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            StartCoroutine(Vanish());
        }
    }

    IEnumerator Vanish()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

}
