using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamageZone : MonoBehaviour
{
    [SerializeField] int Damage = 1;
    [SerializeField] public bool isActive = true;

    private void OnTriggerStay(Collider other)
    {
        if (isActive)
        {
            if (other.CompareTag("Player"))
            {
                print("fasdf");
                var conditionComponent = other.gameObject.GetComponent<ConditionComponent>();
                conditionComponent.TakeDamage(Damage);
                MoveUp(other.gameObject.GetComponent<Rigidbody>());
            }
        }
    }

    public void MoveUp(Rigidbody rb)
    {
        rb.AddForce(transform.up * 0.3f, ForceMode.VelocityChange);
        rb.AddForce(-transform.forward * 0.3f, ForceMode.VelocityChange);
    }
}
