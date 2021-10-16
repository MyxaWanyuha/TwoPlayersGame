using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Vector3 openPos = new Vector3(0, -0.342f, 0.512f);
    Vector3 openRot = new Vector3(-48, 0, 0);

    [SerializeField] GameObject cap;
    [SerializeField] GameObject spawner;
    
    public GameObject[] PickUpItems;

    bool isActivated = false;

    public void Open()
    {
        if (isActivated) return;
        isActivated = true;
        cap.transform.localPosition = openPos;
        cap.transform.localEulerAngles = openRot;
        foreach (var e in PickUpItems)
        {
            SpawnItem(e);
        }
    }

    void SpawnItem(GameObject e)
    {
        var spawnPos = transform.position + new Vector3(0, 1, 0);
        var spawned = Instantiate(spawner, spawnPos, transform.rotation);
        spawned.GetComponent<SpawnPickUp>().spawnObject = e;
        spawned.GetComponent<MeshFilter>().mesh = e.GetComponent<MeshFilter>().sharedMesh;
        spawned.GetComponent<MeshRenderer>().material = e.GetComponent<MeshRenderer>().sharedMaterial;
    }
}
