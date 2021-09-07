using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEndHit : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var e = animator.transform.GetComponent<ConditionComponent>();
        if (e)
        {
            e.IsCanGetDamage = true;
        }
    }
}
