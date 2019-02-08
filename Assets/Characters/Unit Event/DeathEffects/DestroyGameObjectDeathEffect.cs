using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectDeathEffect : DeathEffect
{
    public override void DoDeathEffect()
    {
        UnitTeam unitTeam = GetComponent<UnitTeam>();
        unitTeam.RemoveFromTeam();
        Destroy(gameObject);
    }
}
