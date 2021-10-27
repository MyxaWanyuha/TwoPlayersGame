using UnityEngine;

public class AnimEndAttack : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var attackComp = animator.transform.GetComponent<AttackComponent>();
        if (attackComp)
        {
            attackComp.StopAttack();
        }
    }
}
