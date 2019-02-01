using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTeam : MonoBehaviour
{
    public string name;
    public Team team;

    UnitManager unitManager;

    bool fineOurTime = false;

    private void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        unitManager = gameManager.GetComponent<UnitManager>();
        FindOurTeam();
    }

    //Заменяет свою команду
    public void СhangeTeam(string newTeam) {
        if (unitManager.teams.IndexOf(team) != -1)
        {
            unitManager.teams[unitManager.teams.IndexOf(team)].units.Remove(gameObject);
        }
        name = newTeam;
        FindOurTeam();
    }

    //Довавляет себя в список своей команды
    public void FindOurTeam() {
        for (int t = 0; t < unitManager.teams.Count; t++)
        {
            if (unitManager.teams[t].name == name)
            {
                unitManager.teams[t].units.Add(gameObject);
                fineOurTime = true;
                team = unitManager.teams[t];
            }
        }
        if (!fineOurTime)
        {
            Team ourTeam = new Team(name);
            ourTeam.units.Add(gameObject);
            unitManager.teams.Add(ourTeam);
            team = ourTeam;
        }
    }
}
