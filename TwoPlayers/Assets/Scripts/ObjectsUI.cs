using UnityEngine;

public class ObjectsUI : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] GameObject imageP01;
    [SerializeField] GameObject imageP02;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;
        var p = other.GetComponent<MovementComponent>();
        if (p == null)
            return;

        if (text != null)
        {
            text.gameObject.SetActive(true);
        }

        if (p.isPlayer1 == true)
            imageP01.gameObject.SetActive(true);
        else
            imageP02.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;
        var p = other.GetComponent<MovementComponent>();
        text.gameObject.SetActive(false);
        if (p.isPlayer1 == true)
            imageP01.gameObject.SetActive(false);
        else
            imageP02.gameObject.SetActive(false);
    }
}
