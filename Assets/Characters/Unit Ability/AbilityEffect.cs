﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEffect : ScriptableObject
{
    public abstract void DoAbilityEffect(GameObject target, GameObject owner, GameObject projectile);
}
