using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate: MonoBehaviour
{
    public bool isPressed;

    private void Start()
    {
        isPressed = false;

    }
    private void OnTriggerStay(Collider other)
    {
        isPressed = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isPressed = false; 
    }
}
