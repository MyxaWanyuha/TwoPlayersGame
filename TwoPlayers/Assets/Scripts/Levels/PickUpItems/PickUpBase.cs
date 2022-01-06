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
            var num = Random.Range(0, clips.Length - 1);
            AudioSource.PlayClipAtPoint(clips[num], transform.position, GameController.GetInstance().GetFXVolume());
            Destroy(gameObject);
        }
    }

    protected virtual bool PickUp(Collider other)
    {
        return true;
    }
}
