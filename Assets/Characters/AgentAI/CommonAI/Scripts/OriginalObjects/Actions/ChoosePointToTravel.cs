using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActionInState/ChoosePointToTravel")]
public class ChoosePointToTravel : ActionInState
{
    public override void Act(StateController controller)
    {
        controller.pointToMove.pointToMove = controller.GetComponent<PurposeOfTravel>().purposeOfTravel;
    }
}
