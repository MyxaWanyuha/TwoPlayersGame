using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInput : MonoBehaviour
{
    AttackComponent attack;

    [SerializeField]
    string attackBinding = "Attack";

    bool isAttack = false;

    void Start()
    {
        attack = GetComponent<AttackComponent>();
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
        }
    }
}
