using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTeamToDeadDeathEffect : DeathEffect
{
    public override void DoDeathEffect()
    {
        UnitTeam unitTeam = gameObject.GetComponent<UnitTeam>();
        unitTeam.СhangeTeam("Dead");
    }
}
