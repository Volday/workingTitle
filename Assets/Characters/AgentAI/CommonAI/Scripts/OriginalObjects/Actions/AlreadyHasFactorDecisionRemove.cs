using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/AlreadyHasFactorDecisionRemove")]
public class AlreadyHasFactorDecisionRemove : ActionInState
{
    public override void Act(StateController controller)
    {
        AlreadyHasFactorDecision alreadyHasFactorDecision = controller.gameObject.GetComponent<AlreadyHasFactorDecision>();
        if (alreadyHasFactorDecision != null) {
            Destroy(alreadyHasFactorDecision);
        }
    }
}
