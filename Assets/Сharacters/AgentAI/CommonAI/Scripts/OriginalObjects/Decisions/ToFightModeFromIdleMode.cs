using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decition/ToFightModeFromIdleMode")]
public class ToFightModeFromIdleMode : Decision
{
    public override int Decide(StateController controller)
    {
        return -1;
    }
}
