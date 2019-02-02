using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAbility : MonoBehaviour
{
    public float cooldown = 0;
    [HideInInspector] public float timeAfterLastCast = 0;
    public float rangeCast = 0;
    public float value = 0;

    public Decision decision;

    public bool flyingProjectile = false;
    public bool areaEffect = false;
    public bool pointUse = false;
    [HideInInspector] public Muzzle muzzle;

    public abstract void UseAbility(GameObject abilityTarget);

    public virtual void Start()
    {
        muzzle = GetComponent<Muzzle>();
        UnitAbilities unitAbilities = GetComponent<UnitAbilities>();
        if (unitAbilities != null) {
            unitAbilities.unitAbilities.Add(this);
        }
    }

    private void Update()
    {
        timeAfterLastCast += Time.deltaTime;
    }

    public void CastAbilityEnd() {
        if (gameObject.GetComponent<CastAbilityDone>() == null)
        {
            gameObject.AddComponent<CastAbilityDone>();
        }
    }

    public bool CooldownReady()
    {
        if (timeAfterLastCast > cooldown)
        {
            return true;
        }
        return false;
    }
}
