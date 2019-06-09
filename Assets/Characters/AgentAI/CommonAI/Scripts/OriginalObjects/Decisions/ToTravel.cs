using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/ToTravel")]
public class ToTravel : Decision
{
    public float cautionFactorCoefficient = 1;

    public override float Decide(StateController controller)
    {
        if (controller.gameObject.GetComponent<PurposeOfTravel>().purposeOfTravel != new Vector3(float.MinValue, float.MinValue, float.MinValue)) {
            Vector3 spawnPosition = controller.GetComponent<PurposeOfTravel>().purposeOfTravel;
            Vector3 differenceVector = spawnPosition - controller.transform.position;
            float newDistance = (differenceVector.x * differenceVector.x) + (differenceVector.y * differenceVector.y) + (differenceVector.z * differenceVector.z);
            if (newDistance > 4) {
                return float.MaxValue;
            }
        }
        return float.MinValue;
    }
}
