using UnityEngine;

public class MeleeAttack : UnitAbility
{
    public override void UseAbility(GameObject abilityTarget)
    {
        if (timeAfterLastCast > cooldown) {
            Vector3 differenceVector = abilityTarget.transform.position - transform.position;
            if ((differenceVector.x * differenceVector.x) + (differenceVector.y * differenceVector.y) + (differenceVector.z * differenceVector.z) < rangeCast * rangeCast) {
                gameObject.AddComponent<CastAbilityTime>();
                HealthPoints targetHP = abilityTarget.GetComponent<HealthPoints>();
                if (targetHP != null) {
                    targetHP.currentHealthPoints -= Damage;
                    timeAfterLastCast = 0;
                }
            }
        }

        if (gameObject.GetComponent<CastAbilityDone>() == null) {
            gameObject.AddComponent<CastAbilityDone>();
        }
    }
}