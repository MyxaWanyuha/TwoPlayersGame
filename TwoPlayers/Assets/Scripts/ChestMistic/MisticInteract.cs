using UnityEngine;

public class MisticInteract : MonoBehaviour
{
    enum PlayerIndex { Nan, P1, P2 };
    PlayerIndex player = PlayerIndex.Nan;

    AIChest mistic;

    private void Start()
    {
        mistic = GetComponent<AIChest>();
    }

    private void Update()
    {
        if (player == PlayerIndex.P1 && Input.GetButton("Interact") || player == PlayerIndex.P2 && Input.GetButton("Interact2"))
        {
            mistic.Activate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;
        var p = other.GetComponent<MovementComponent>();
        if (p != null)
        {
            player = p.isPlayer1 ? PlayerIndex.P1 : PlayerIndex.P2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player = PlayerIndex.Nan;
    }
}
