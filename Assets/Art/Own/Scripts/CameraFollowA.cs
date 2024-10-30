using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowA : MonoBehaviour
{
    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private Transform followedObject;
    public GameObject Boulder;
    public int lever = 1;

    void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.tag == "t_trigger")
        {
            lever = 0;
            Boulder.SetActive(true);
            
        }
        if (other.gameObject.tag == "t_end")
        {
            lever = 1;
            Boulder.SetActive(false);
        }
    }
    private void LateUpdate()
    {
        if (lever == 0)
        {
            transform.position = followedObject.position + Vector3.up * (distanceUp - 1) - followedObject.forward * (-1 * distanceAway - 1);
            transform.LookAt(followedObject);
        }
        if (lever == 1)
        {
            transform.position = followedObject.position + Vector3.up * distanceUp - followedObject.forward *  distanceAway;
            transform.LookAt(followedObject);
        }

    }

}
