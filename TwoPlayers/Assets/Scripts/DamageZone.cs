using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] int Damage = 1;
    [SerializeField] string TagDamage = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagDamage))
        {
            var conditionComponent = other.gameObject.GetComponent<ConditionComponent>();
            conditionComponent.TakeDamage(Damage);
        }
    }
}
