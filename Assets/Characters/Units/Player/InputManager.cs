using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public UnitAbilities unitAbilities;

    void Start()
    {
        unitAbilities = GetComponent<UnitAbilities>();
    }

    void Update()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            for (int t = 0; t < unitAbilities.unitAbilities.Count; t++) {
                unitAbilities.unitAbilities[t].UseAbility(gameObject);
            }
        }
    }
}
