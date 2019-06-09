using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/ChooseAbility")]
public class ChooseAbility : ActionInState
{
    public override void Act(StateController controller)
    {
        if (controller.targetToAttack.targetToAttack != null) {
            float maxValue = float.MinValue;
            UnitAbility nextUnitAbility = null;
            for (int t = 0; t < controller.unitAbilities.unitAbilities.Count; t++) {
                controller.abilityPending = controller.unitAbilities.unitAbilities[t];
                float nextValue = controller.unitAbilities.unitAbilities[t].decision.Decide(controller);

                if (nextValue > maxValue)
                {
                    maxValue = nextValue;
                    nextUnitAbility = controller.unitAbilities.unitAbilities[t];
                }
            }
            controller.nextUnitAbility = nextUnitAbility;
        }
    }
}
