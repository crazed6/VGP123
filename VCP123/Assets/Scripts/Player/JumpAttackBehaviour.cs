using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackBehavor : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
