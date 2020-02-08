using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollowBehavior : StateMachineBehaviour
{
    private AIController controller;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.gameObject.GetComponent<AIController>();
        controller.NavAgent.SetDestination(controller.CurrentTarget.position);
        Debug.Log("Follow");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (controller.CurrentTarget != null)
        {
            controller.NavAgent.SetDestination(controller.CurrentTarget.position);
        }
        else if (controller.NavAgent.remainingDistance < 1.0f && controller.CurrentTarget == null)
        {
            animator.SetBool("mIsFollowing", false);
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        controller.NavAgent.ResetPath();
    }

}
