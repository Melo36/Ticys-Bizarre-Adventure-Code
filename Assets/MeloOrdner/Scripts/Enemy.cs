using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public bool invincible;
    [SerializeField]
    public float bumpSpeed=0;
    public float speed;
    Rigidbody enemyRigidbody;
    void Awake()
    {
        enemyRigidbody = gameObject.GetComponent<Rigidbody>(
       );
    }
    void FixedUpdate()
    {
        enemyRigidbody.velocity = new Vector3(speed, enemyRigidbody.velocity.y, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "End")
        {
            speed *= -1;
        }
    }

    public void OnDeath()
    {
        gameObject.GetComponent<Collider>
        ().enabled = false;
    }
}
