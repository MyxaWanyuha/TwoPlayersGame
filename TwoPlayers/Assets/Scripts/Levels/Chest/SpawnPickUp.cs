using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickUp : MonoBehaviour
{
    public GameObject spawnObject;

    void Start()
    {
        var sign = Random.Range(0, 2) == 0 ? 1 : -1;
        var forward = -1 * transform.up;
        var right = transform.right;
        var up = transform.forward;
        var f = up
            + forward * Random.Range(0, 2)
            + sign * right * Random.Range(0, 2);
        GetComponent<Rigidbody>().AddForce(f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn") || other.CompareTag("Coin") || other.CompareTag("Health")) return;
        if (spawnObject)
        {
            var rot = new Quaternion();
            rot.eulerAngles = new Vector3(-90, 0, 0);
            Instantiate(spawnObject, transform.position + new Vector3(0, 0.5f, 0), rot);
        }
        spawnObject = null;
        Destroy(gameObject);
    }
}
