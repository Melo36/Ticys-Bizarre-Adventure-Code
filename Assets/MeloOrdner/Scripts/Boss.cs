using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float chaseSpeed;
    [SerializeField]
    private float normalSpeed;
    [SerializeField]
    private GameObject Prey;
    [SerializeField]
    GameObject p;

   
    public float getNormalSpeed()
    {
        return normalSpeed;
    }

    public float getChaseSpeed()
    {
        return chaseSpeed;
    }

    private void Start()
    {
        Prey = GameObject.Find("Player");
    }

    private void Update()
    {
        bosslive();
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag=="Bomb")
        {
            
            GameData.bosslive--;
            Instantiate(p, collision.transform.position ,Quaternion.identity);
            StartCoroutine(speed0());
            Prey.GetComponent<PlayerPhysicMe>().bombSpawn(collision.gameObject);
        }
    }

    IEnumerator speed0()
    {
        float oldNormalSpeed = normalSpeed;
        float oldChaseSpeed = chaseSpeed;
        normalSpeed = 0;
        chaseSpeed = 0;
        yield return new WaitForSeconds(2);
        normalSpeed = oldNormalSpeed;
        chaseSpeed = oldChaseSpeed;

    }

    private void bosslive()
    {
        if(GameData.bosslive==0)
        {
            GameData.loadNextScene();
        }
    }
}
