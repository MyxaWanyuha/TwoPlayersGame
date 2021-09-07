using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    MovementComponent movement;
    AttackComponent attack;
    ConditionComponent condition;

    [SerializeField]
    string attackBinding = "Attack";

    bool isAttack = false;

    void Start()
    {
        movement = GetComponent<MovementComponent>();
        attack = GetComponent<AttackComponent>();
        condition = GetComponent<ConditionComponent>();
    }

    void Update()
    {
        isAttack = Input.GetButton(attackBinding);
    }

    private void FixedUpdate()
    {
        if (isAttack)
        {
            attack.StartAttack();
            movement.IsCanMovingJumping = false;
        }
    }
}
