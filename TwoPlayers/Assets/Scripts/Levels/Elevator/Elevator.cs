using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Elevator parameters")]
    float minY;
    [SerializeField] float maxY;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] bool isAnyButton;
    [Header("Buttons links")]
    [SerializeField] PressurePlate[] buttons;

    void Start()
    {
        minY = gameObject.transform.position.y;
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
        if (currentPos.y > minY && currentPos.y < maxY)
                transform.position = currentPos;
    }

    private void MoveDown()
    {
        Vector3 currentPos = transform.position;
        currentPos.y = currentPos.y - moveSpeed * Time.deltaTime;
        if (currentPos.y < maxY && currentPos.y > minY)
                transform.position = currentPos;
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
