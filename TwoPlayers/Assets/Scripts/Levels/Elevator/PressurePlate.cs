using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private double minY;
    private Vector3 maxY;
    private bool  isStay;
    public bool isPressed;
    [SerializeField] float pressureSpeed = 5;
    private void Start()
    {
        isPressed = false;
        maxY = this.transform.localPosition;
        minY = maxY.y - 2.3f;
    }

    private void OnTriggerStay(Collider other)
    {
        isStay = true;
        if (this.transform.localPosition.y > minY)
        {
            transform.localPosition -= pressureSpeed * maxY * Time.deltaTime;
        }
        isPressed = true;
    }
    private void Update()
    {
        if (!isStay && transform.localPosition.y < maxY.y)
        {
            transform.localPosition += pressureSpeed * maxY * Time.deltaTime;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isStay = false;
        isPressed = false; 
    }
}
