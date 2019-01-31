using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/ToFightModeFromDoAbility")]
public class ToFightModeFromDoAbility : Decision
{
    public override float Decide(StateController controller)
    {
        CastAbilityDone castAbilityDone = controller.gameObject.GetComponent<CastAbilityDone>();
        if (castAbilityDone != null)
        {
            Destroy(castAbilityDone);
            CastAbilityTime castAbilityTime = controller.gameObject.GetComponent<CastAbilityTime>();
            if (castAbilityTime != null)
            {
                Destroy(castAbilityTime);
            }
            return float.MaxValue;
        }
        else {
            return float.MinValue;
        }
    }
}
