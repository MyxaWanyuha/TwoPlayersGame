using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] float minY;
    [SerializeField] float maxY;
    [SerializeField] float moveSpeed;
    bool isMoveUp = false;
    bool isStopped = false;
    bool temp = false;
    [SerializeField]  float timeLeft = 2;
    private void Start()
    {
        moveSpeed = 1;
        minY = transform.localPosition.y;
        maxY = minY + 0.33f;
    }

    private void MoveUp()
    {
        Vector3 currentPos = transform.position;
        currentPos.y = currentPos.y + 3 * Time.deltaTime;
        transform.position = currentPos;
        if (gameObject.transform.localPosition.y > maxY)
        {
            isMoveUp = false;
            moveSpeed = 1f;
        }
    }

    private void MoveDown()
    {
        Vector3 currentPos = transform.position;
        currentPos.y = currentPos.y - 1 * Time.deltaTime;
        transform.position = currentPos;
        if (gameObject.transform.localPosition.y < minY)
        {
            isStopped = true;
            isMoveUp = true;
            moveSpeed = 40;
        }

    }
    
    IEnumerator Wait(float second)
    {
        temp = true;
        yield return new WaitForSeconds(second);
        isStopped = false;
        temp = false;
    }
    private void Update()
    {
        if (!isStopped)
        {
            if (isMoveUp)
            {
                MoveUp();
            }
            else
            {
                MoveDown();
            }
        }
        else if (!temp)
        {
            StartCoroutine(Wait(2.0f));

        }

    }
    IEnumerator ExecuteAfterTime(float timeInSec)
    {
        yield return new WaitForSeconds(timeInSec);
        isMoveUp = true;
        moveSpeed = 0.05f;
        //сделать нужное
    }
}
