using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChest : AIBase
{
    void Start()
    {
        Init();
        conditionComponent.IsCanGetDamage = false;
        currentState = AIState.Chase;
    }

    void Update()
    {
        if (CheckDead())
            return;

        if (conditionComponent.IsTryGetDamage == true)
        {
            if (conditionComponent.IsCanGetDamage == false)
            {
                conditionComponent.IsCanGetDamage = true;
                animator.SetTrigger("Activation");
            }
        }
        else return;

        switch (currentState)
        {
            case AIState.Chase:
                Chase(GetNearestPlayer());
                break;
            case AIState.Attack:
                Attack(GetNearestPlayer());
                break;
            case AIState.Patrol:
                Chase(GetNearestPlayer());
                break;
        }
    }
}
