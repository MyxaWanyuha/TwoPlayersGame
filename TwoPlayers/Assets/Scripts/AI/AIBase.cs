using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AIBase : MonoBehaviour
{
    ConditionComponent conditionComponent;
    NavMeshAgent navMeshAgent;
    Animator animator;
    Transform player1;
    Transform player2;

    enum AIState
    {
        Idle, Patrol, Chase, Hit, Attack
    }

    AIState currentState = AIState.Idle;

    [SerializeField] Patrol[] patrolPoints;
    int curPatrolIndex = -1;

    float waitTimer = 0;
    [SerializeField] float timeBetweenAttack = 1.0f;

    float visibleDist = 10.0f;
    float visisbleAngle = 90.0f;
    float meleeDist = 1.5f;

    void Start()
    {
        conditionComponent = GetComponent<ConditionComponent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (patrolPoints.Length != 0)
            ChangeState(AIState.Patrol);

        var players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length != 2)
        {
            print("Error: players count " + players.Length.ToString());
        }
        else
        {
            player1 = players[0].transform;
            player2 = players[1].transform;
        }
        animator.SetBool("IsPatrolling", true);

        if (CanSeePlayer(GetNearestPlayer()))
        {
            ChangeState(AIState.Chase);
        }
        else if (Random.Range(0, 100) < 10)
        {
            ChangeState(AIState.Patrol);
        }
    }

    void Update()
    {
        animator.SetFloat("Speed", navMeshAgent.speed);
        var nearestPlayer = GetNearestPlayer();
        switch (currentState)
        {
            case AIState.Patrol:
                if (navMeshAgent.remainingDistance < 1)
                {
                    if (curPatrolIndex >= patrolPoints.Length - 1)
                    {
                        curPatrolIndex = 0;
                    }
                    else
                    {
                        ++curPatrolIndex;
                    }
                    navMeshAgent.SetDestination(patrolPoints[curPatrolIndex].transform.position);
                }
                
                if (CanSeePlayer(nearestPlayer))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Chase:
                navMeshAgent.SetDestination(nearestPlayer.position);
                if (navMeshAgent.hasPath)
                {
                    if (CanAttackPlayer(nearestPlayer))
                    {
                        ChangeState(AIState.Attack);
                    }
                    else if (CanStopChase(nearestPlayer))
                    {
                        ChangeState(AIState.Patrol);
                    }
                }
                break;
            case AIState.Attack:
                LookPlayer(2.0f, nearestPlayer);

                waitTimer += Time.deltaTime;
                if (waitTimer >= timeBetweenAttack)
                {
                    if (CanAttackPlayer(nearestPlayer))
                        ChangeState(AIState.Chase);
                    else
                        ChangeState(AIState.Patrol);
                }
                break;
        }
    }

    Transform GetNearestPlayer()
    {
        var sqrDistP1 = (player1.position - transform.position).sqrMagnitude;
        var sqrDistP2 = (player2.position - transform.position).sqrMagnitude;
        return sqrDistP1 < sqrDistP2 ? player1 : player2;
    }

    bool CanSeePlayer(Transform player)
    {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        return direction.magnitude < visibleDist && angle < visisbleAngle;
    }

    bool CanAttackPlayer(Transform player)
    {
        Vector3 direction = player.position - transform.position;
        return direction.magnitude < meleeDist;
    }

    bool CanStopChase(Transform player)
    {
        Vector3 direction = player.position - transform.position;
        return direction.magnitude > visibleDist;
    }

    void ChangeState(AIState newState)
    {
        switch (newState)
        {
            case AIState.Patrol:
                animator.SetBool("IsPatrolling", true);
                navMeshAgent.speed = 2;
                navMeshAgent.isStopped = false;

                float lastDist = Mathf.Infinity;
                for (int i = 0; i < patrolPoints.Length; i++)
                {
                    var thisWP = patrolPoints[i];
                    float distance = Vector3.Distance(transform.position, thisWP.transform.position);
                    if (distance < lastDist)
                    {
                        curPatrolIndex = i - 1; //Because in the update it will be added one
                        lastDist = distance;
                    }
                }
                break;
            case AIState.Chase:
                navMeshAgent.speed = 3;
                navMeshAgent.isStopped = false;
                animator.SetBool("IsPatrolling", false);
                break;
            case AIState.Attack:
                navMeshAgent.isStopped = true;
                waitTimer = 0;
                break;
            case AIState.Hit:
                navMeshAgent.isStopped = true;
                waitTimer = 0;
                break;
        }
        currentState = newState;
    }

    void LookPlayer(float speedRot, Transform player)
    {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        direction.y = 0;

        if (direction != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * speedRot);
    }
}
