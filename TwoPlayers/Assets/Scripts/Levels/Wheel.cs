using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] float speed; //скорость туда-сюда
    [SerializeField] Vector3 vector;
    void Update()
    {
        transform.Rotate(vector.x * speed * Time.deltaTime, vector.y * speed * Time.deltaTime, vector.z * speed * Time.deltaTime); 
    }
}
