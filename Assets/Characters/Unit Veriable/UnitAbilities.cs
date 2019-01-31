using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbilities : MonoBehaviour
{
    public List<UnitAbility> unitAbilities;

    void Start()
    {
        unitAbilities = new List<UnitAbility>(GetComponents<UnitAbility>());
    }
}
