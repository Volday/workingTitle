using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/DoEvasion")]
public class DoEvasion : ActionInState
{
    public override void Act(StateController controller)
    {
        controller.navMeshAgent.destination = controller.pointEscape;
    }
}
