using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("ѕараметры лифта")]
    [SerializeField]float minY;
    [SerializeField]float maxY;
    [SerializeField]float moveSpeed ;

    [Header("—сылки на кнопки")]
    [SerializeField] Button btnOnePressed;
    [SerializeField] Button btnTwoPressed;


    void Start()
    {
        btnTwoPressed.isPressed = false;
        btnOnePressed.isPressed = false;
        minY = gameObject.transform.position.y;
        maxY = minY + 11.29f;
        moveSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (btnOnePressed.isPressed || btnTwoPressed.isPressed)
        {
            Vector3 currentPos = transform.position;
            currentPos.y = currentPos.y + moveSpeed * Time.deltaTime;
            if (currentPos.y > minY)
                if (currentPos.y < maxY)
                    this.transform.position = currentPos;
        }
        else
        {
            Vector3 currentPos = transform.position;
            currentPos.y = currentPos.y - moveSpeed * Time.deltaTime;
            if (currentPos.y < maxY)
                if (currentPos.y > minY)
                    this.transform.position = currentPos;
        }


        

    }
}
