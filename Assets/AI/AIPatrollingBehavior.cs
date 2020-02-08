using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrollingBehavior : StateMachineBehaviour
{
    private AIController controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.gameObject.GetComponent<AIController>();
        Debug.Log("Patrol");
        //generate random target position and start moving towards it
        var targetPos = RandomNavSphere(animator.transform.position, 50.0f, -1);

        controller.NavAgent.SetDestination(targetPos);
        controller.UpdateRotation();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.UpdateRotation();
        //change state when finding a target or when close to the target pos
        animator.SetBool("mIsFollowing", controller.CurrentTarget != null);
        animator.SetBool("mIsPatrolling", controller.NavAgent.remainingDistance >= 2.0f);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.NavAgent.velocity = Vector3.zero;
        controller.NavAgent.ResetPath();
    }

    /// <summary>
    /// Get random position on the walkable mesh
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="dist"></param>
    /// <param name="layermask"></param>
    /// <returns></returns>
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        //generate a random target position close to the GO 
        Vector3 randDirection = Random.insideUnitCircle * dist;
        randDirection += origin;

        //get a valid position as close as possible to the target
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }



    public static Vector3 RandomNavSphereRaycast(Vector3 origin, float dist, int layermask)
    {
        var pos = RandomNavSphere(origin, dist, layermask);

        //get direction and distance
        Vector3 dirToTarget = pos - origin;
        float distToTarget = Vector3.Distance(origin, pos);

        Debug.Log("Direction   " + dirToTarget);
        Debug.Log("Distance    " + dist);
        Debug.DrawRay(origin, dirToTarget, Color.red);
        //if the raycast does not hit anything, return the position
        if (!Physics.Raycast(origin, dirToTarget, distToTarget))
        {
            Debug.Log("Final Position:  " + pos);
            return pos;
        }
        //otherwise calculate it again

        return RandomNavSphere(origin, dist, layermask);

    }

}
