using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] float minZ;
    [SerializeField] float maxZ;
    [SerializeField] float forwardSpeed = 1;
    [SerializeField] float backSpeed = 1;
    [SerializeField] bool isMoveForward = true;

    private void MoveForward()
    {
        transform.localPosition += transform.forward * forwardSpeed * Time.deltaTime;
        if (transform.localPosition.z >= maxZ)
        {
            isMoveForward = !isMoveForward;
        }
    }

    private void MoveBack()
    {
        transform.localPosition -= transform.forward * backSpeed * Time.deltaTime;
        if (transform.localPosition.z <= minZ)
        {
            isMoveForward = !isMoveForward;
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
