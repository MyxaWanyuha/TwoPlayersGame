using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] Collider[] colliders;

    public bool IsAttack { get; private set; }

    private void Start()
    {
        StopAttack();
    }

    public void StartAttack()
    {
        IsAttack = true;
        foreach (var e in colliders)
        {
            e.enabled = true;
        }
    }

    public void StopAttack()
    {
        IsAttack = false;
        foreach (var e in colliders)
        {
            e.enabled = false;
        }
    }
}
