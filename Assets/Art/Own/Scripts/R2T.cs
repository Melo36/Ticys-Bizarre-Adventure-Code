using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R2T : MonoBehaviour
{
    public GameObject player;
    public GameObject Übergang;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Übergang1());
        }

    }
    IEnumerator Übergang1()
    {
        Übergang.SetActive(true);
        yield return new WaitForSeconds(2);
        player.transform.position = new Vector3(-5475, 31, 182);
        yield return new WaitForSeconds(2);
        Übergang.SetActive(false);
    }
}
