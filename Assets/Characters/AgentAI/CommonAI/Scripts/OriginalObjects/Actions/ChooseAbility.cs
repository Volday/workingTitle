using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/ChooseAbility")]
public class ChooseAbility : ActionInState
{
    public override void Act(StateController controller)
    {
        float maxValue = float.MinValue;
        UnitAbility nextUnitAbility = null;
        AlreadyHasFactorDecision alreadyHasFactorDecision = controller.gameObject.AddComponent<AlreadyHasFactorDecision>();
        for (int t = 0; t < controller.unitAbilities.unitAbilities.Count; t++) {
            for (int i = 0; i < controller.unitAbilities.unitAbilities.Count; i++)
            {
                float nextValue = alreadyHasFactorDecision.GetFactorValue(controller.unitAbilities.unitAbilities[t].decision);
                if (nextValue == float.MinValue)
                {
                    controller.abilityPending = controller.unitAbilities.unitAbilities[t];
                    nextValue = controller.unitAbilities.unitAbilities[t].decision.Decide(controller);
                    alreadyHasFactorDecision.SetFactorValue(controller.unitAbilities.unitAbilities[t].decision, nextValue);
                }
                if (nextValue > maxValue)
                {
                    maxValue = nextValue;
                    nextUnitAbility = controller.unitAbilities.unitAbilities[t];
                }
            }
        }
        Debug.Log(nextUnitAbility);
        Destroy(alreadyHasFactorDecision);
        controller.nextUnitAbility = nextUnitAbility;
    }
}
