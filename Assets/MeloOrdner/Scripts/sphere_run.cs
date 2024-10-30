using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere_run : StateMachineBehaviour
{
    Rigidbody enemyRigidbody;
    Transform player;
    Transform boss;
    float speed;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyRigidbody=animator.GetComponent<Rigidbody>();
        player=GameObject.FindGameObjectWithTag("Player").transform;
        boss = GameObject.FindGameObjectWithTag("Boss").transform;
        speed = animator.GetComponent<Boss>().getNormalSpeed();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = GameObject.FindGameObjectWithTag("Boss").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ChaseLineOfSight(player.position, speed);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private void ChaseLineOfSight(Vector3 targetPosition, float Speed)
    {
        Vector3 direction = targetPosition - boss.transform.position; direction.Normalize();
        enemyRigidbody.velocity = new Vector3(direction.x * Speed, enemyRigidbody.velocity.y, direction.z * Speed);
    }

}
