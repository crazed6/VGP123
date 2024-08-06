using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackBehavor : MonoBehaviour
{

    void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
