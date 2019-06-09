using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/ToIdleModeFromReturn")]
public class ToIdleModeFromReturn : Decision
{
    public override float Decide(StateController controller)
    {
        Vector3 spawnPosition = controller.GetComponent<PurposeOfTravel>().purposeOfTravel;
        Vector3 differenceVector = spawnPosition - controller.transform.position;
        float newDistance = (differenceVector.x * differenceVector.x) + (differenceVector.y * differenceVector.y) + (differenceVector.z * differenceVector.z);
        if (newDistance < 4)
        {
            return float.MaxValue;
        }
        return float.MinValue;
    }
}
