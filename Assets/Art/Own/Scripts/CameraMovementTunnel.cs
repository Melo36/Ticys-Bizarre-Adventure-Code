using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementTunnel : MonoBehaviour
{

    Animator camera_Animator;
    public GameObject Boulder;

    private void Start()
    {
        camera_Animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
    if (other.gameObject.tag == "t_trigger")
        {
            camera_Animator.SetTrigger("ani_tunnel");
            Boulder.SetActive(true);
        }
    if (other.gameObject.tag == "t_end")
        {
            camera_Animator.ResetTrigger("ani_tunnel_end");
            Boulder.SetActive(false);
        }
    }

}
