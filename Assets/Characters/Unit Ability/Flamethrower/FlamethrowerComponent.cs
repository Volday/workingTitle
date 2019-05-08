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
            GetComponent<NavMeshAgent>().destination = stateController.targetToAttack.targetToAttack.transform.position;
        }
    }

    public override void CastEnd()
    {
        GetComponent<MoveSpeed>().moveSpeed /= slowdownCoefficient;
        Destroy(this);
    }
}
