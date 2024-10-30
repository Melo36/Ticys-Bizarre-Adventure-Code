using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_pattern : StateMachineBehaviour
{
    private Transform boss;
    [SerializeField]
    private float distanceThreshold;
    [SerializeField]
    private float ChaseEvadeDistance;
    private float chaseSpeed;
    private Rigidbody enemyRigidbody;
    private List<Waypoint> wayPoints;
    private int currentWayPoint = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Rigidbody>().transform;
        chaseSpeed = animator.GetComponent<Boss>().getChaseSpeed();
        enemyRigidbody = animator.GetComponent<Rigidbody>();
        wayPoints = GameObject.Find("Player").GetComponent<PlayerPhysicMe>().GetWaypoints();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chaseSpeed = animator.GetComponent<Boss>().getChaseSpeed();
    }

  

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    void PatternMovement()
    {
        ChaseLineOfSight(wayPoints[currentWayPoint].transform.position, chaseSpeed);
        //Check if we are close to the next waypoint and incerement to the next waypoint.
        if (Vector3.Distance(boss.position, wayPoints[currentWayPoint].transform.position) < distanceThreshold)
        {
            currentWayPoint = (currentWayPoint + 1)% wayPoints.Count; //modulo to restart at the beginning.
        }
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PatternMovement();
    }

    private void ChaseLineOfSight(Vector3 targetPosition, float Speed)
    {
        Vector3 direction = targetPosition - boss.position; direction.Normalize();
        enemyRigidbody.velocity = new Vector3(direction.x * Speed, enemyRigidbody.velocity.y, direction.z * Speed);
    }
}
