using System;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsUI : MonoBehaviour
{
    [SerializeField] Text text;
    Action func;

    enum PlayerIndex { Nan, P1, P2};
    PlayerIndex player = PlayerIndex.Nan;

    private void Start()
    {
        var isAI = GetComponent<AIChest>();
        if(isAI)
        {
            func = isAI.Activate;
        }
        else
        {
            var isChest = GetComponent<Chest>();
            func = isChest.Open;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        text.gameObject.SetActive(true);
        var p = other.GetComponent<MovementComponent>();
        if (p)
        {
            player = p.isPlayer1 ? PlayerIndex.P1 : PlayerIndex.P2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.gameObject.SetActive(false);
        player = PlayerIndex.Nan;
    }

    private void Update()
    {
        if(player == PlayerIndex.P1)
        {
            if(Input.GetButton("Interact"))
            {
                func();
            }
        }
        else if(player == PlayerIndex.P2)
        {
            if(Input.GetKey("/") || Input.GetButton("Interact2"))
            {
                func();
            }
        }
    }
}
