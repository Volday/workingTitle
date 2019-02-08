using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/DecideInTime")]
public class DecideInTime : Decision
{
    public float timeToDicide;
    public override float Decide(StateController controller)
    {
        if (controller.GetComponent<DecideInTimeComponent>() == null) {
            DecideInTimeComponent decideInTimeComponent = controller.gameObject.AddComponent<DecideInTimeComponent>();
            decideInTimeComponent.StartDecide(timeToDicide);
        }else if(controller.GetComponent<DecideInTimeComponent>().readyDecide) {
            return float.MaxValue;
        }

        return float.MinValue;
    }
}
