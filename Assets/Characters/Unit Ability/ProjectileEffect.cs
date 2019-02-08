using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileEffect : ScriptableObject
{
    public abstract void DoProjectileEffect(GameObject carrier);
}
