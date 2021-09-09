using UnityEngine;

public class Elevator : MonoBehaviour
{
    float minY;
    [Header("Elevator parameters")]
    [SerializeField] float maxY;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] bool isAnyButton;
    [Header("Buttons links")]
    [SerializeField] PressurePlate[] buttons;

    void Start()
    {
        minY = transform.localPosition.y;
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
        Vector3 currentPos = transform.localPosition;
        currentPos.y = currentPos.y + moveSpeed * Time.deltaTime;
        if (currentPos.y > minY && currentPos.y < maxY)
                transform.localPosition = currentPos;
    }

    private void MoveDown()
    {
        Vector3 currentPos = transform.localPosition;
        currentPos.y = currentPos.y - moveSpeed * Time.deltaTime;
        if (currentPos.y < maxY && currentPos.y > minY)
                transform.localPosition  = currentPos;
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
