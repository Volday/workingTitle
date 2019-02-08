using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateGameObjectDeathEffect : DeathEffect
{
    public override void DoDeathEffect()
    {
        gameObject.SetActive(false);
    }
}
