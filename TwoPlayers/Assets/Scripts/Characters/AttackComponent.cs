using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] Collider[] colliders;

    private void Start()
    {
        StopAttack();
    }

    public void StartAttack()
    {
        foreach(var e in colliders)
        {
            e.enabled = true;
        }
    }

    public void StopAttack()
    {
        foreach (var e in colliders)
        {
            e.enabled = false;
        }
    }
}
