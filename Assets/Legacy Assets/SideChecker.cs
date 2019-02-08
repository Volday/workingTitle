using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideChecker : MonoBehaviour
{
    void Update()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        UnitManager unitManager = gameManager.GetComponent<UnitManager>();
        for (int t = 0; t < unitManager.projectiles.Count; t++)
        {
            Debug.Log(MyMath.PointToRightOfLine(new Vector2(transform.position.x, transform.position.z),
                new Vector2(unitManager.projectiles[t].transform.forward.x + unitManager.projectiles[t].transform.position.x,
                unitManager.projectiles[t].transform.forward.z + unitManager.projectiles[t].transform.position.z),
                new Vector2(unitManager.projectiles[t].transform.position.x, unitManager.projectiles[t].transform.position.z)));
        }
    }
}
