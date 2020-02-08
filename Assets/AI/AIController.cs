using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public float MoveSpeed = 5;
    public float TurnSpeed = 3;

    private AIFieldOfView fieldOfView;
    public NavMeshAgent NavAgent;

    public Transform CurrentTarget
    {
        get
        {
            return fieldOfView.PlayerTarget;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        fieldOfView = GetComponent<AIFieldOfView>();
        NavAgent.speed = MoveSpeed;
        NavAgent.angularSpeed = TurnSpeed;
    }

    // Update is called once per frame
    public void UpdateRotation()
    {
        var moveDirection = NavAgent.destination - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), TurnSpeed * Time.deltaTime);
        
    }


}
