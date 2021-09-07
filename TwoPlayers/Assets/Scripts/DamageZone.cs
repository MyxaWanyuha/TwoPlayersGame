using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] int Damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var conditionComponent = other.gameObject.GetComponent<ConditionComponent>();
            conditionComponent.TakeDamage(Damage);
            //conditionComponent.MoveUp();
        }
    }
}
