using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsUI : MonoBehaviour
{
    [SerializeField] Text text;
    AIChest chest;
    private void Start()
    {
        chest = gameObject.GetComponent<AIChest>();
    }
    // на нажатие кнопки вызвать chest.Activate();
    private void OnTriggerEnter(Collider other)
    {
        text.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        text.gameObject.SetActive(false);
    }
}
