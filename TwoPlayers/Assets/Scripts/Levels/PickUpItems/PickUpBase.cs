using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PickUp(other))
        {
            Destroy(gameObject);
        }
    }

    protected virtual bool PickUp(Collider other)
    {
        return true;
    }
}
