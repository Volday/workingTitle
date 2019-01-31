using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTeam : MonoBehaviour
{
    public string name;
    public Team team;

    bool fineOurTime = false;

    private void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        UnitManager unitManager = gameManager.GetComponent<UnitManager>();
        for (int t = 0; t < unitManager.teams.Count; t++) {
            if (unitManager.teams[t].name == name) {
                unitManager.teams[t].units.Add(gameObject);
                fineOurTime = true;
                team = unitManager.teams[t];
            }
        }
        if (!fineOurTime) {
            Team ourTeam = new Team(name);
            ourTeam.units.Add(gameObject);
            unitManager.teams.Add(ourTeam);
            team = ourTeam;
        }
    }
}
