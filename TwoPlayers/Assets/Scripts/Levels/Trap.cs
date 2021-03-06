using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    float minY;
    float maxY;
    bool isMoveUp = false;
    bool isStopped = false;
    TrapDamageZone trapDamageZone;
    bool temp = false;
    [SerializeField] float delay;
    [SerializeField]  float cooldown = 2;
    private void Start()
    {
        trapDamageZone = gameObject.GetComponent<TrapDamageZone>();
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
        }
    }
    
    IEnumerator Wait(float second)
    {
        trapDamageZone.isActive = false;
        temp = true;
        yield return new WaitForSeconds(second);
        isStopped = false;
        temp = false;
        trapDamageZone.isActive = true;
    }
    private void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else
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
                StartCoroutine(Wait(cooldown));

            }
        }

    }

}
