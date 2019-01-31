using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Action/CastNextAbility")]
public class CastNextAbility : ActionInState
{
    public override void Act(StateController controller)
    {
        CastAbilityTime castAbilityTime = controller.gameObject.GetComponent<CastAbilityTime>();
        if (controller.nextUnitAbility != null && castAbilityTime == null) {
            controller.nextUnitAbility.UseAbility(controller.targetToAttack.targetToAtack);
        }
    }
}
