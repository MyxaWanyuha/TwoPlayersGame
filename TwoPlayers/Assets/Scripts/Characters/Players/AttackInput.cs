using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInput : MonoBehaviour
{

    [SerializeField]
    string attack = "Attack";

    AttackComponent attackComponent;

    bool isAttack = false;
    private void Start()
    {
        attackComponent = gameObject.GetComponent<AttackComponent>();
    }

    void Update()
    {
        isAttack = Input.GetButton(attack);
    }

    private void FixedUpdate()
    {
        if (isAttack)
        {
            attackComponent.StartAttack();
        }
    }
}
