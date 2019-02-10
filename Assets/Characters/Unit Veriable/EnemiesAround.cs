using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAround : MonoBehaviour
{
    UnitManager unitManager;
    public List<GameObject> enemiesAround;
    public float distanceToCheck = 30;

    private void Awake()
    {
        enemiesAround = new List<GameObject>();
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        unitManager = gameManager.GetComponent<UnitManager>();
    }

    public int FindEnemiesAround() {
        enemiesAround.Clear();
        string myTeam = GetComponent<UnitTeam>().name;
        for (int i = 0; i < unitManager.teams.Count; i++)
        {
            if (unitManager.teams[i].name != myTeam && unitManager.teams[i].name != "Dead")
            {
                for (int t = 0; t < unitManager.teams[i].units.Count; t++)
                {
                    if (unitManager.teams[i].units[t].GetComponent<Creature>() != null) {
                        Vector3 differenceVector = unitManager.teams[i].units[t].transform.position - transform.position;
                        float newDistance = (differenceVector.x * differenceVector.x) + (differenceVector.z * differenceVector.z);
                        if (newDistance < distanceToCheck * distanceToCheck)
                        {
                            enemiesAround.Add(unitManager.teams[i].units[t]);
                        }
                    }
                }
            }
        }
        return enemiesAround.Count;
    }

    public List<GameObject> FindEnemiesAround(float searchRadius)
    {
        enemiesAround.Clear();
        string myTeam = GetComponent<UnitTeam>().name;
        for (int i = 0; i < unitManager.teams.Count; i++)
        {
            if (unitManager.teams[i].name != myTeam && unitManager.teams[i].name != "Dead")
            {
                for (int t = 0; t < unitManager.teams[i].units.Count; t++)
                {
                    if (unitManager.teams[i].units[t].GetComponent<Creature>() != null)
                    {
                        Vector3 differenceVector = unitManager.teams[i].units[t].transform.position - transform.position;
                        float newDistance = (differenceVector.x * differenceVector.x) + (differenceVector.z * differenceVector.z);
                        if (newDistance < searchRadius * searchRadius)
                        {
                            enemiesAround.Add(unitManager.teams[i].units[t]);
                        }
                    }
                }
            }
        }
        return enemiesAround;
    }

    public List<GameObject> FindTargetsAroundInRadius(float radius)
    {
        List<GameObject> targetsAroundInRadius = new List<GameObject>();
        for (int i = 0; i < unitManager.teams.Count; i++)
        {
            for (int t = 0; t < unitManager.teams[i].units.Count; t++)
            {
                Vector3 differenceVector = unitManager.teams[i].units[t].transform.position - transform.position;
                float newDistance = (differenceVector.x * differenceVector.x) + (differenceVector.z * differenceVector.z);
                if (newDistance < radius * radius)
                {
                    targetsAroundInRadius.Add(unitManager.teams[i].units[t]);
                }
            }
        }
        return targetsAroundInRadius;
    }
}
