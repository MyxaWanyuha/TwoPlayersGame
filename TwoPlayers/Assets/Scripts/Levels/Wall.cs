using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] float minZ;
    [SerializeField] float maxZ;
    [SerializeField] float moveSpeed;
    bool isMoveForward = true;
    void Start()
    {
        minZ = gameObject.transform.position.z;
        maxZ = minZ - 6.14f;
        moveSpeed = 5;
    }

    private void MoveForward()
    {
        Vector3 currentPos = transform.localPosition;
        currentPos.z = currentPos.z - moveSpeed * Time.deltaTime;
        transform.position = currentPos;
        if (gameObject.transform.localPosition.z < maxZ)
        {
            isMoveForward = false;
            moveSpeed = 1f;
        }
    }

    private void MoveBack()
    {
        Vector3 currentPos = transform.localPosition;
        currentPos.z = currentPos.z + moveSpeed * Time.deltaTime;
        transform.position = currentPos;
        if (gameObject.transform.localPosition.z > minZ)
        {
            isMoveForward = true;
            moveSpeed = 25f;
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
