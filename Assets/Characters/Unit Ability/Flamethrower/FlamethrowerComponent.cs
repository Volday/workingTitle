using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlamethrowerComponent : AbilityComponent
{
    StateController stateController;
    float slowdownCoefficient = 0.5f;
    NavMeshAgent navMeshAgent;

    private void Start()
    {
        GetComponent<MoveSpeed>().moveSpeed *= slowdownCoefficient;
        stateController = GetComponent<StateController>();
    }

    void Update()
    {
        if (stateController.isActiveAndEnabled) {
            float enemyRadius = stateController.targetToAttack.targetToAttack.GetComponent<CapsuleCollider>().radius;
            float myRadius = GetComponent<CapsuleCollider>().radius;
            if (MyMath.sqrDistanceFromPointToPoint(stateController.targetToAttack.targetToAttack.transform.position, transform.position) >
                enemyRadius * enemyRadius + myRadius * myRadius + 9)
            {
                stateController.navMeshAgent.destination = stateController.targetToAttack.targetToAttack.transform.position;
            }
            else {
                stateController.navMeshAgent.destination = transform.position;
                transform.LookAt(stateController.targetToAttack.targetToAttack.transform.position);
            }
        }
    }

    public override void CastEnd()
    {
        GetComponent<MoveSpeed>().moveSpeed /= slowdownCoefficient;
        Destroy(this);
    }
}
