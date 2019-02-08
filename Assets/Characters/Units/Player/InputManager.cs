using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public UnitAbilities unitAbilities;
    Camera mainCamera;

    float nexttimeAfterLastClick = 0;

    void Start()
    {
        mainCamera = Camera.main;
        unitAbilities = GetComponent<UnitAbilities>();
    }

    void Update()
    {
        nexttimeAfterLastClick += Time.deltaTime;
        if (Input.GetAxis("Fire1") > 0 && nexttimeAfterLastClick > 0.1f)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); ;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                for (int t = 0; t < unitAbilities.unitAbilities.Count; t++)
                {
                    GameObject emptyTarget = new GameObject();
                    emptyTarget.transform.position = hit.point;
                    unitAbilities.unitAbilities[t].UseAbility(emptyTarget);
                }
            }
        }
    }
}
