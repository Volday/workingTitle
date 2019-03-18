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

    public List<GameObject> FindClosestEnemies(int count)
    {
        List<GameObject> closestEnemies = new List<GameObject>();
        List <float> closestEnemiesDistance = new List<float>();

        for (int t = 0; t < count; t++) {
            closestEnemies.Add(new GameObject());
            closestEnemiesDistance.Add(float.MaxValue);
        }

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

                        float maxDistanceToClosestEnemies = float.MinValue;
                        for (int j = 0; j < closestEnemiesDistance.Count; j++) {
                            if (closestEnemiesDistance[j] > maxDistanceToClosestEnemies) {
                                maxDistanceToClosestEnemies = closestEnemiesDistance[j];
                            }
                        }

                        if (newDistance < maxDistanceToClosestEnemies) {
                            int oldElementIndex = closestEnemiesDistance.IndexOf(maxDistanceToClosestEnemies);
                            closestEnemies[oldElementIndex] = unitManager.teams[i].units[t];
                            closestEnemiesDistance[oldElementIndex] = newDistance;
                        }
                    }
                }
            }
        }
        List<GameObject> FindedClosestEnemies = new List<GameObject>();

        for (int t = 0; t < closestEnemiesDistance.Count; t++) {
            if (closestEnemiesDistance[t] != float.MaxValue) {
                FindedClosestEnemies.Add(closestEnemies[t]);
            }
        }

        return FindedClosestEnemies;
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
