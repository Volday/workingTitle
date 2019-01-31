using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecideInTime")]
public class DecideInTime : Decision
{
    public float timeToDicide;
    float currentTimeToDicide;
    public override float Decide(StateController controller)
    {
        if (currentTimeToDicide > timeToDicide)
        {
            return float.MaxValue;
        }
        else {
            currentTimeToDicide += 0.2f;
        }
        return float.MinValue;
    }
}
