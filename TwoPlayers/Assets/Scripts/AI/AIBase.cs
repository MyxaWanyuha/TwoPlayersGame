using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AIBase : MonoBehaviour
{
    protected ConditionComponent conditionComponent;
    protected Animator animator;
    NavMeshAgent navMeshAgent;
    Transform player1;
    Transform player2;
    [SerializeField] BoxCollider boxCollider;
    protected enum AIState
    {
        Idle, Patrol, Chase, Hit, Attack
    }

    protected AIState currentState = AIState.Idle;

    [SerializeField] Patrol[] patrolPoints;
    int curPatrolIndex = -1;

    float waitTimer = 0;
    [SerializeField] float timeStopToAttack = 1.0f;

    float visibleDist = 10.0f;
    float visisbleAngle = 90.0f;
    float meleeDist = 1.5f;

    void Start()
    {
        Init();

        if (patrolPoints.Length != 0)
            ChangeState(AIState.Patrol);
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

    protected void Init()
    {
        conditionComponent = GetComponent<ConditionComponent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

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

    bool isWasDead = false;
    protected bool CheckDead()
    {
        if (conditionComponent.isDead == true)
        {
            if (isWasDead == false)
            {
                boxCollider.enabled = false;
                animator.Play("Die");
                var info = animator.GetCurrentAnimatorStateInfo(0);
                Destroy(gameObject, info.length);
                isWasDead = true;
            }
            return true;
        }
        return false;
    }

    void Update()
    {
        if (CheckDead())
            return;

        animator.SetFloat("Speed", navMeshAgent.speed);
        var nearestPlayer = GetNearestPlayer();
        switch (currentState)
        {
            case AIState.Patrol:
                Patrol(nearestPlayer);
                break;
            case AIState.Chase:
                Chase(nearestPlayer);
                break;
            case AIState.Attack:
                Attack(nearestPlayer);
                break;
        }
    }

    protected void Attack(Transform nearestPlayer)
    {
        LookPlayer(2.0f, nearestPlayer);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
        {
            animator.Play("Attack");
            Ray ray = new Ray(animator.transform.position + new Vector3(0, 1.0f, 0), animator.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2.5f) && hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<ConditionComponent>().TakeDamage(1);
            }
        }

        waitTimer += Time.deltaTime;
        if (waitTimer >= timeStopToAttack)
        {
            if (CanAttackPlayer(nearestPlayer))
                ChangeState(AIState.Chase);
            else
                ChangeState(AIState.Patrol);
        }
    }

    protected void Chase(Transform nearestPlayer)
    {
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
    }

    private void Patrol(Transform nearestPlayer)
    {
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
    }

    protected Transform GetNearestPlayer()
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
        direction.y = 0;

        if (direction != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * speedRot);
    }
}
