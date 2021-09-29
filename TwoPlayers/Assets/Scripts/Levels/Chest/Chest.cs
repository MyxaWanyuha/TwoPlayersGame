using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Vector3 openPos = new Vector3(0, -0.342f, 0.512f);
    Vector3 openRot = new Vector3(-48, 0, 0);

    [SerializeField] GameObject cap;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Open();
        }
    }

    void Open()
    {
        cap.transform.localPosition = openPos;
        cap.transform.localEulerAngles = openRot;
        //TODO spawn items
    }
}
