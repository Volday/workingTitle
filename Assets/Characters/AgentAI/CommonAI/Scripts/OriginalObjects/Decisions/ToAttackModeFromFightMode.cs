using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/ToAttackModeFromFightMode")]
public class ToAttackModeFromFightMode : Decision
{
    public override float Decide(StateController controller)
    {
        return 100;
    }
}
