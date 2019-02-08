using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/RemoveDecideInTimeComponent")]
public class RemoveDecideInTimeComponent : ActionInState
{
    public override void Act(StateController controller)
    {
        DecideInTimeComponent decideInTimeComponent = controller.GetComponent<DecideInTimeComponent>();
        if (decideInTimeComponent != null) {
            Destroy(decideInTimeComponent);
        }
    }
}
