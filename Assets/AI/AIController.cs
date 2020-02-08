using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public float MoveSpeed = 5;
    public float TurnSpeed = 9;

    private AIFieldOfView fieldOfView;
    private EnemyAttackController enemyAttackController;
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
        enemyAttackController = GetComponent<EnemyAttackController>();
        NavAgent.speed = MoveSpeed;
        NavAgent.angularSpeed = TurnSpeed;
    }

    // Update is called once per frame
    public void UpdateRotation()
    {
        var destination = CurrentTarget == null ? NavAgent.destination : CurrentTarget.position;
        var moveDirection =  destination - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), TurnSpeed * Time.deltaTime);       
    }

    public void Shoot()
    {
        enemyAttackController.Shoot(CurrentTarget);
    }

}
