using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/ToFightModeFromIdleMode")]
public class ToFightModeFromIdleMode : Decision
{
    public override float Decide(StateController controller)
    {
        for (int i = 0; i < controller.unitManager.teams.Count; i++)
        {
            if (controller.unitManager.teams[i].name != controller.unitTeam.name && controller.unitManager.teams[i].name != "Dead")
            {
                for (int t = 0; t < controller.unitManager.teams[i].units.Count; t++)
                {
                    if (controller.unitManager.teams[i].units[t].GetComponent<Creature>() != null)
                    {
                        Vector3 differenceVector = controller.unitManager.teams[i].units[t].transform.position - controller.transform.position;
                        if ((differenceVector.x * differenceVector.x) + (differenceVector.z * differenceVector.z) < controller.radiusOfView.radiusOfView * controller.radiusOfView.radiusOfView)
                        {
                            return float.MaxValue;
                        }
                    }
                }
            }
        }
        return float.MinValue;
    }
}
