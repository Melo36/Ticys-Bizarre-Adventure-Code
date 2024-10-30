using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_jump : StateMachineBehaviour
{
    private Rigidbody enemyRigidbody;
    Transform player;
    Transform boss;
    private float speed;
    Transform bossp;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossp=GameObject.FindGameObjectWithTag("Boss").transform;
        boss = animator.GetComponent<Rigidbody>().transform;
        enemyRigidbody = animator.GetComponent<Rigidbody>();
        speed = animator.GetComponent<Boss>().getNormalSpeed();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ChaseLineOfSight(player.position, speed);


    }

    private void ChaseLineOfSight(Vector3 targetPosition, float Speed)
    {
        Vector3 direction = targetPosition - boss.transform.position; direction.Normalize();
        enemyRigidbody.velocity = new Vector3(direction.x * Speed, enemyRigidbody.velocity.y, direction.z * Speed);
    }

    

    
}
