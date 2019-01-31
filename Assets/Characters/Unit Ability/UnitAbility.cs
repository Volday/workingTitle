using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAbility : MonoBehaviour
{
    public float cooldown = 0;
    [HideInInspector] public float timeAfterLastCast = 0;

    public float rangeCast = 0;

    public float Damage = 0;

    public abstract void UseAbility(GameObject abilityTarget);

    private void Update()
    {
        timeAfterLastCast += Time.deltaTime;
    }
}
