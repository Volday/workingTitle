using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/CastNextAbility")]
public class CastNextAbility : ActionInState
{
    public override void Act(StateController controller)
    {
        CastAbilityTime castAbilityTime = controller.gameObject.GetComponent<CastAbilityTime>();
        if (castAbilityTime == null && controller.nextUnitAbility != null) {
            controller.nextUnitAbility.UseAbility(controller.targetToAttack.targetToAttack);
        }
    }
}
