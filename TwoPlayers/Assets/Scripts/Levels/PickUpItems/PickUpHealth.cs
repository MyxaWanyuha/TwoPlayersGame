using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealth : PickUpBase
{
    [SerializeField] int Count = 1;
    protected override bool PickUp(Collider other)
    {
        return other.GetComponent<ConditionComponent>().Healing(Count);
    }
}
