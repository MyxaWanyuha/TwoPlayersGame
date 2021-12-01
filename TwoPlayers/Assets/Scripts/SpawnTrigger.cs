using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField]
    Vector3 SpawnP;
    public static event SetSpawnPoint setSpawnPoint;
    public  delegate void SetSpawnPoint(Vector3 sp);
    private void Start()
    {
        SpawnP = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Player detected");
            setSpawnPoint?.Invoke(SpawnP);
            this.gameObject.SetActive(false);
        }
    }
}
