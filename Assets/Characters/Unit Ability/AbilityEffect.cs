using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEffect : ScriptableObject
{
    public GameObject owner;

    public abstract void DoAbilityEffect();
}
