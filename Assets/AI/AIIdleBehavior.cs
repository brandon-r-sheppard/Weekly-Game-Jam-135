using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleBehavior : StateMachineBehaviour
{
    float idleTime = 2.0f;
    float currentTime = 0.0f;

    private AIController controller;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.gameObject.GetComponent<AIController>();
        Debug.Log("Idle");
        idleTime = Random.Range(0.5f, 3.0f);
        currentTime = 0.0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentTime += Time.deltaTime;
        animator.SetBool("mIsFollowing", controller.CurrentTarget != null);
        animator.SetBool("mIsPatrolling", currentTime >= idleTime);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

}
