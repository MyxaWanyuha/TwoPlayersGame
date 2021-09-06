using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("��������� �����")]
    [SerializeField]float minY;
    [SerializeField]float maxY;
    [SerializeField]float moveSpeed ;
    [SerializeField] bool isAnyButton;
    [Header("������ �� ������")]
    [SerializeField] PressurePlate[] buttons;


    void Start()
    {
        minY = gameObject.transform.position.y;
        maxY = minY + 11.29f;
        moveSpeed = 1;
    }

    private bool IsAnyButtonPressed()
    {
        foreach (var button in buttons)
        {
            if (button.isPressed)
                return true;
        }
        return false;
    }
    private bool IsAllButtonsPressed()
    {
        foreach (var button in buttons)
        {
            if (!button.isPressed)
                return false;
        }
        return true;
    }
    private void MoveUp()
    {
        Vector3 currentPos = transform.position;
        currentPos.y = currentPos.y + moveSpeed * Time.deltaTime;
        if (currentPos.y > minY)
            if (currentPos.y < maxY)
                this.transform.position = currentPos;
    }

    private void MoveDown()
    {
        Vector3 currentPos = transform.position;
        currentPos.y = currentPos.y - moveSpeed * Time.deltaTime;
        if (currentPos.y < maxY)
            if (currentPos.y > minY)
                this.transform.position = currentPos;
    }
    void Update()
    {
        if (isAnyButton)
        {
            if (IsAnyButtonPressed())
            {
                MoveUp();
            }
            else
            {
                MoveDown();
            }
        }
        else
        {
            if (IsAllButtonsPressed())
            {
                MoveUp();
            }
            else
            {
                MoveDown();
            }
        }

    }
}
