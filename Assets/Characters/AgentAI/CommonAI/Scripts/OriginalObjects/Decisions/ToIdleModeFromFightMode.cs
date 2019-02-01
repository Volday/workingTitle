﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/ToIdleModeFromFightMode")]
public class ToIdleModeFromFightMode : Decision
{
    public override float Decide(StateController controller)
    {
        bool haveEnemy = true;
        for (int i = 0; i < controller.unitManager.teams.Count; i++)
        {
            if (controller.unitManager.teams[i].name != controller.unitTeam.name)
            {
                for (int t = 0; t < controller.unitManager.teams[i].units.Count; t++)
                {
                    Vector3 differenceVector = controller.unitManager.teams[i].units[t].transform.position - controller.transform.position;
                    if ((differenceVector.x * differenceVector.x) + (differenceVector.z * differenceVector.z) > controller.radiusOfView.radiusOfView * controller.radiusOfView.radiusOfView)
                    {
                        haveEnemy = false;
                    }
                }
            }
        }
        if (haveEnemy) {
            controller.navMeshAgent.destination = controller.transform.position;
            return float.MaxValue;
        }
        else {
            return float.MinValue;
        }
    }
}
