using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AIBase : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Animator animator;
    Transform player1;
    Transform player2;

    enum AIState
    {
        Idle, Patrol, Chase, Hit, Attack
    }

    AIState currentState = AIState.Idle;

    //[SerializeField] List<Patrol> patrolPoints = new List<Patrol>();
    [SerializeField] Patrol[] patrolPoints;
    int curPatrolIndex = -1; //The point of the patrol points where the enemy goes

    float waitTimer = 0;
    [SerializeField] float timeBetweenAttack = 1.0f;

    float visibleDist = 10.0f;
    float visisbleAngle = 90.0f;
    float meleeDist = 1.5f;

    void Start()
    {
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
    }

    void Update()
    {
        var nearestPlayer = GetNearestPlayer();
        switch (currentState)
        {
            case AIState.Idle:
                if (CanSeePlayer(nearestPlayer))
                {
                    ChangeState(AIState.Chase);
                }
                else if (Random.Range(0, 100) < 10)
                {
                    ChangeState(AIState.Patrol);
                }
                break;
            case AIState.Patrol:
                if (navMeshAgent.remainingDistance < 1)
                {
                    if (curPatrolIndex >= patrolPoints.Length - 1)
                        curPatrolIndex = 0;
                    else
                        curPatrolIndex++;
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
            case AIState.Hit:
                waitTimer += Time.deltaTime;
                if (waitTimer < 0.5f)
                {
                    LookPlayer(5.0f, nearestPlayer);
                }
                //else if (waitTimer >= 0.5f/* && isInvincible*/)
                //{
                //    isInvincible = false;
                //}
                else if (waitTimer >= 1.25f)
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
        //TODO
        return player1;
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

    bool CanStopChase(Transform player) //Stop follow the player
    {
        Vector3 direction = player.position - transform.position;
        return direction.magnitude > visibleDist;
    }

    void ChangeState(AIState newState)
    {
        switch (currentState)
        {
            case AIState.Idle:
                //animator.ResetTrigger("isIdle");
                break;
            case AIState.Patrol:
                //animator.ResetTrigger("isPatrolling");
                break;
            case AIState.Chase:
                //animator.ResetTrigger("isChasing");
                break;
            case AIState.Attack:
                //animator.ResetTrigger("isMeleeAttacking");
                //animEv.isAttacking = false;
                //animator.GetComponent<AnimatorEventsEn>().DisableWeaponColl();
                break;
            case AIState.Hit:
                //animator.ResetTrigger("isHited");
                break;
        }
        switch (newState)
        {
            case AIState.Idle:
                //anim.SetTrigger("isIdle");
                break;
            case AIState.Patrol:
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
                //anim.SetTrigger("isPatrolling");
                break;
            case AIState.Chase:
                navMeshAgent.speed = 3;
                navMeshAgent.isStopped = false;
                //anim.SetTrigger("isChasing");
                break;
            case AIState.Attack:
                //animator.SetTrigger("isMeleeAttacking");
                navMeshAgent.isStopped = true;
                waitTimer = 0;
                //animEv.isAttacking = true;
                break;
            case AIState.Hit:
                //anim.SetTrigger("isHited");
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
