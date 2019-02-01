using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/ChooseTargetForAttack")]
public class ChooseTargetForAttack : ActionInState
{
    float minDistance;
    public override void Act(StateController controller)
    {
        if (controller.gameObject.GetComponent<EnemyFindAlready>() == null) {
            controller.gameObject.AddComponent<EnemyFindAlready>();
            minDistance = float.MaxValue;
            controller.enemiesAround.enemiesAround.Clear();
            for (int i = 0; i < controller.unitManager.teams.Count; i++)
            {
                if (controller.unitManager.teams[i].name != controller.unitTeam.name)
                {
                    for (int t = 0; t < controller.unitManager.teams[i].units.Count; t++)
                    {
                        Vector3 differenceVector = controller.unitManager.teams[i].units[t].transform.position - controller.transform.position;
                        float newDistance = (differenceVector.x * differenceVector.x) + (differenceVector.z * differenceVector.z);
                        if (newDistance < controller.radiusOfView.radiusOfView * controller.radiusOfView.radiusOfView)
                        {
                            controller.enemiesAround.enemiesAround.Add(controller.unitManager.teams[i].units[t]);
                            if (newDistance < minDistance)
                            {
                                controller.targetToAttack.targetToAtack = controller.unitManager.teams[i].units[t];
                                minDistance = newDistance;
                            }
                        }
                    }
                }
            }
        }
    }
}
