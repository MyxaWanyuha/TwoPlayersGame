using UnityEngine;

public class AnimEndAttack : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var e = animator.transform.GetComponent<AttackComponent>();
        if (e)
        {
            e.StopAttack();
        }
    }
}
