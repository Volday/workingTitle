using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTeam : MonoBehaviour
{
    public string name;
    public Team team;

    UnitManager unitManager;

    private void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        unitManager = gameManager.GetComponent<UnitManager>();
        FindOurTeam();
        if (GetComponent<Projectile>() != null) {
            unitManager.projectiles.Add(gameObject);
        }
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

    //Удаляет объект
    public void RemoveFromTeam()
    {
        if (unitManager.teams.IndexOf(team) != -1)
        {
            unitManager.teams[unitManager.teams.IndexOf(team)].units.Remove(gameObject);
        }
    }

    //Довавляет себя в список своей команды
    public void FindOurTeam() {
        bool fineOurTime = false;
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
