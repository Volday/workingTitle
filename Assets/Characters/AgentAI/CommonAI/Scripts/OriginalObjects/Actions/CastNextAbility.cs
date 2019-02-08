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
            GameObject abilityTarget = new GameObject();
            abilityTarget.transform.position = controller.futureTargetPosition;
            controller.nextUnitAbility.UseAbility(abilityTarget);
        }
    }
}
