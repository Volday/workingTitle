using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieWhenTargetHitPEComponent : MonoBehaviour
{
    
    void Update()
    {
        if (GetComponent<TargetHitPE>() != null) {
            GetComponent<Death>().Die();
        }    
    }
}
