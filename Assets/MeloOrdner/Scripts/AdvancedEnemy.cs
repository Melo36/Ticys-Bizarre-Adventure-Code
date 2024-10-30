using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdvancedEnemy : MonoBehaviour
{
    [SerializeField]
    private float ChaseSpeed;
    [SerializeField]
    private float NormalSpeed;
    [SerializeField]
    private GameObject Prey;
    private Rigidbody enemyRigidbody;

    [SerializeField]
    private List<Waypoint> wayPoints;
    private int currentWayPoint = 0;
    [SerializeField]
    private float distanceThreshold;
    [SerializeField]
    private float ChaseEvadeDistance;

    NavMeshAgent agent;

    void PatternMovement()
    {
        ChaseLineOfSight(wayPoints[currentWayPoint].transform.position, NormalSpeed);
        //Check if we are close to the next waypoint and incerement to the next waypoint.
        if (Vector3.Distance(gameObject.transform.position, wayPoints[currentWayPoint].transform.position) < distanceThreshold)
        {
            currentWayPoint = (currentWayPoint + 1) % wayPoints.Count; //modulo to restart at the beginning.
        }
    }

    private bool PlayerVisible(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - gameObject.transform.position;
        directionToTarget.Normalize();
        RaycastHit hit;
        Physics.Raycast(gameObject.transform.position, directionToTarget, out hit);
        return hit.collider.gameObject.tag.Equals("Player");
    }

    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        enemyRigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.destination = wayPoints[currentWayPoint].transform.position;
    }

    private void NavigateToNextPoint()
    {
        currentWayPoint = (currentWayPoint + 1) % wayPoints.Count; agent.destination =
        wayPoints[currentWayPoint].transform.position;
    }
    public enum Behaviour
    {
        LineOfSight,
        Intercept,
        PatternMovement,
        ChasePatternMovement,
        Hide,
        PatternMovementNavMesh
    }
    public Behaviour behaviour;

    private void FixedUpdate()
    {
        switch (behaviour)
        {
            case Behaviour.LineOfSight:
                ChaseLineOfSight(Prey.transform.position, ChaseSpeed);//Exercise 1
                break;
            case Behaviour.Intercept:
                Intercept(Prey.transform.position);//Exercise 2
                break;
            case Behaviour.PatternMovement:
                PatternMovement();//Exercise 3
                break;
            case Behaviour.ChasePatternMovement:
                if (Vector3.Distance(gameObject.transform.position, Prey.transform.position) < ChaseEvadeDistance)
                {
                    ChaseLineOfSight(Prey.transform.position, ChaseSpeed);
                }
                else
                {
                    PatternMovement();
                }
                break; //Exercise 4
               
            case Behaviour.Hide:
                if (PlayerVisible(Prey.transform.position))
                {
                    ChaseLineOfSight(Prey.transform.position, ChaseSpeed);
                }
                else
                {
                    PatternMovement();
                }
                break;
            case Behaviour.PatternMovementNavMesh:
                if (!agent.pathPending &&
                 agent.remainingDistance < 0.5f)
                    NavigateToNextPoint();
                break; //Exercise 5
            default:
                break;
        }
    }
    private void ChaseLineOfSight(Vector3 targetPosition, float Speed)
    {
        Vector3 direction = targetPosition - transform.position; direction.Normalize();
        enemyRigidbody.velocity = new Vector3(direction.x * Speed, enemyRigidbody.velocity.y, direction.z * Speed);
    }

    private void Intercept(Vector3 targetPosition)
    {
        Vector3 enemyPosition =
    gameObject.transform.position;
        Vector3 PreyPosition =
        Prey.transform.position;
        Vector3 VelocityRelative, Distance, PredictedInterceptionPoint;
        float timeToClose;

        VelocityRelative = Prey.GetComponent<Rigidbody>().velocity - enemyRigidbody.velocity;
        Distance = targetPosition - enemyPosition;
        timeToClose = Distance.magnitude / VelocityRelative.magnitude;
        PredictedInterceptionPoint = targetPosition + (timeToClose * Prey.GetComponent<Rigidbody>().velocity);
        Vector3 direction = PredictedInterceptionPoint - enemyPosition;
        direction.Normalize();
        enemyRigidbody.velocity = new Vector3(direction.x * ChaseSpeed, enemyRigidbody.velocity.y, direction.z * ChaseSpeed);
    }
}
