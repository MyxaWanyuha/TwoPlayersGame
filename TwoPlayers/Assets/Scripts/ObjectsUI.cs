using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsUI : MonoBehaviour
{
    [SerializeField] Text text;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        text.gameObject.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        text.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
