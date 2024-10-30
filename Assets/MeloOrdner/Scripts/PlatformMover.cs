using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField]
    private Transform pointA;
    [SerializeField]
    private Transform pointB;
    [SerializeField]
    private float speed = 2;
    private float distance = 0;

    private Transform currentTarget;
    private Transform currentStart;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = pointA.position;
        currentTarget = pointB;
        currentStart = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        //moves this obejct towards target position
        distance += speed * Time.deltaTime;
        transform.position = Vector3.Lerp(currentStart.position, currentTarget.position, distance);
        if(distance>1)
        {
            //switch direction
            distance = 0;
            Transform oldTarget = currentTarget;
            currentTarget = currentStart;
            currentStart = oldTarget;
        }
    }
}
