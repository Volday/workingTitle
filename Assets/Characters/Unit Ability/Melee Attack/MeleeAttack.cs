using UnityEngine;

public class MeleeAttack : UnitAbility
{
    public float damage = 10; 

    public override void UseAbility(GameObject abilityTarget)
    {
        if (timeAfterLastCast > cooldown) {
            Vector3 differenceVector = abilityTarget.transform.position - transform.position;
            if ((differenceVector.x * differenceVector.x) + (differenceVector.y * differenceVector.y) + (differenceVector.z * differenceVector.z) < rangeCast * rangeCast) {
                HealthPoints targetHP = abilityTarget.GetComponent<HealthPoints>();
                if (targetHP != null) {
                    targetHP.TakeDamage(damage);
                    timeAfterLastCast = 0;
                }
            }
        }

        CastAbilityEnd();
    }
}