using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] float minZ;
    [SerializeField] float maxZ;
    [SerializeField] float forwardSpeed;
    [SerializeField] float backSpeed;
    bool isMoveForward = true;
    void Start()
    {

    }

    private void MoveForward()
    {
        Vector3 currentPos = transform.localPosition;
        currentPos.z = currentPos.z - forwardSpeed * Time.deltaTime;
        transform.position = currentPos;
        if (gameObject.transform.localPosition.z < maxZ)
        {
            isMoveForward = false;
        }
    }

    private void MoveBack()
    {
        Vector3 currentPos = transform.localPosition;
        currentPos.z = currentPos.z + backSpeed * Time.deltaTime;
        transform.position = currentPos;
        if (gameObject.transform.localPosition.z > minZ)
        {
            isMoveForward = true;
        }
    }

    void Update()
    {

        if (isMoveForward)
        {
            MoveForward();
        }
        else
        {
            MoveBack();
        }
    }
}
