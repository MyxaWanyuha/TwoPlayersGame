using UnityEngine;

public class PickUpCoin : PickUpBase
{
    [SerializeField] int Points = 10;
    public int GetPoints() => Points;
    protected override bool PickUp(Collider other)
    {
        GameController.GetInstance().AddPoints(Points);
        print(GameController.GetInstance().GetPoints().ToString() + " / " + GameController.GetInstance().GetMaxPoints());
        return true;
    }
}
