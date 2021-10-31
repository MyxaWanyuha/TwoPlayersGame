using System;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsUI : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] GameObject imageP01;
    [SerializeField] GameObject imageP02;
    Action func;
    //enum PlayerIndex { Nan, P1, P2};
    //PlayerIndex player = PlayerIndex.Nan;

    private void Start()
    {
        //var isAI = GetComponent<AIChest>();
        //if (isAI)
        //{
        //    func = isAI.Activate;
        //}
        //else
        //{
        //    var isChest = GetComponent<Chest>();
        //    func = isChest.Open;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        text.gameObject.SetActive(true);
        if (other.tag == "Player1")
            imageP01.gameObject.SetActive(true);
        if (other.tag == "Player2")
            imageP02.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        text.gameObject.SetActive(false);
        if (other.tag == "Player1")
            imageP01.gameObject.SetActive(false);
        if (other.tag == "Player2")
            imageP02.gameObject.SetActive(false);
        //player = PlayerIndex.Nan;
    }

    private void Update()
    {
        //if (player == PlayerIndex.P1)
        //{
        //    if (Input.GetButton("Interact"))
        //    {
        //        func();
        //    }
        //}
        //else if (player == PlayerIndex.P2)
        //{
        //    if (Input.GetButton("Interact2"))
        //    {
        //        func();
        //    }
        //}
    }
}
