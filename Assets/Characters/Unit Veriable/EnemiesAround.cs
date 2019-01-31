using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAround : MonoBehaviour
{
    UnitManager unitManager;
    public List<GameObject> enemiesAround;

    private void Awake()
    {
        enemiesAround = new List<GameObject>();
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        unitManager = gameManager.GetComponent<UnitManager>();
    }

    public void FindEnemiesAround() {
        enemiesAround.Clear();
        string myTeam = GetComponent<UnitTeam>().name;
        for (int i = 0; i < unitManager.teams.Count; i++)
        {
            if (unitManager.teams[i].name != myTeam)
            {
                for (int t = 0; t < unitManager.teams[i].units.Count; t++)
                {
                    Vector3 differenceVector = unitManager.teams[i].units[t].transform.position - transform.position;
                    float newDistance = (differenceVector.x * differenceVector.x) + (differenceVector.z * differenceVector.z);
                    if (newDistance < 625)
                    {
                        enemiesAround.Add(unitManager.teams[i].units[t]);
                    }
                }
            }
        }
    }
}
