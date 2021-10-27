using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] float distance = 20;
    [SerializeField] float speed = 10;
    [SerializeField] float delay = 0;

    Vector3 startPos;
    float currentDistance = 0;
    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else if (currentDistance < distance)
        {
            transform.localPosition -= transform.right * speed * Time.deltaTime;
            currentDistance += speed * Time.deltaTime;
        }
        else
        {
            transform.localPosition = startPos;
            currentDistance = 0;
        }
    }
}
