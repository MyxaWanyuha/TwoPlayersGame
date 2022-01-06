using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBase : MonoBehaviour
{
    [SerializeField] GameObject particle;
    [SerializeField] AudioClip[] clips;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PickUp(other))
        {
            if (particle)
            {
                Instantiate(particle, transform.position, transform.rotation);
            }
            GameController.GetInstance().PlaySound(clips);
            Destroy(gameObject);
        }
    }

    protected virtual bool PickUp(Collider other)
    {
        return true;
    }
}
