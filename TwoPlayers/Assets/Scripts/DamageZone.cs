using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] int Damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag[0] == 'P')
        {
            other.gameObject.GetComponent<ConditionComponent>().TakeDamage(Damage);
        }
    }
}
