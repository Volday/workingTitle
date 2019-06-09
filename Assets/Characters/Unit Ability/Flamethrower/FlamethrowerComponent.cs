using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlamethrowerComponent : AbilityComponent
{
    StateController stateController;
    float slowdownCoefficient = 0.5f;

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
                GetComponent<NavMeshAgent>().destination = stateController.targetToAttack.targetToAttack.transform.position;
            }
            else {
                GetComponent<NavMeshAgent>().destination = transform.position;
            }
        }
    }

    public override void CastEnd()
    {
        GetComponent<MoveSpeed>().moveSpeed /= slowdownCoefficient;
        Destroy(this);
    }
}
