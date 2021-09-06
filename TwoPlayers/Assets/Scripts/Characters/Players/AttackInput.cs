using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInput : MonoBehaviour
{

    [SerializeField]
    string attack = "Attack";

    AttackComponent attackComponent;
    private void Start()
    {
        attackComponent = gameObject.GetComponent<AttackComponent>();
    }

    void Update()
    {
        Input.GetButton(attack);
    }

    private void FixedUpdate()
    {
        attackComponent.StartAttack();
    }
}
