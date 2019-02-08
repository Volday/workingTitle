using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/ToFightModeFromDoAbility")]
public class ToFightModeFromDoAbility : Decision
{
    public override float Decide(StateController controller)
    {
        CastAbilityTime castAbilityTime = controller.gameObject.GetComponent<CastAbilityTime>();
        if (castAbilityTime == null)
        {
            return float.MaxValue;
        }
        else {
            return float.MinValue;
        }
    }
}
