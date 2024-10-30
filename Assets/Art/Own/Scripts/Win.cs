using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag ==
        "Player")
        {
            StartCoroutine(WaitWin());


        }


    }

    IEnumerator WaitWin()
    {
        yield return new WaitForSeconds(3);
        GameData.loadNextScene();
    }
}
