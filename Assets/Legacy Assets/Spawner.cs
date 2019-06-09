using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    UnitManager unitManager;
    TimeManager timeManager;
    public string ourTeam;
    public string enemyTeam;
    public GameObject unit;
    public GameObject target;
    public int maxUnitCount = 5;
    System.Random prng;
    void Start()
    {
        prng = new System.Random(5738);
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        unitManager = gameManager.GetComponent<UnitManager>();
        timeManager = gameManager.GetComponent<TimeManager>();
        timeManager.AddAction(GOGOGo, 1, this);
    }

    void GOGOGo()
    {
        timeManager.AddAction(GOGOGo, 1, this);
        bool has = false;
        for (int t = 0; t < unitManager.teams.Count; t++) {
            if (unitManager.teams[t].name == ourTeam) {
                has = true;
                if (unitManager.teams[t].units.Count < maxUnitCount) {
                    GameObject newUnit = Instantiate(unit, transform.position, transform.rotation);
                    newUnit.GetComponent<UnitTeam>().name = ourTeam;
                    //newUnit.GetComponent<UnitTeam>().FindOurTeam();
                    newUnit.GetComponent<MoveSpeed>().moveSpeed = prng.Next(4, 10);
                    AISkills AISkills = newUnit.GetComponent<AISkills>();
                    AISkills.aggression = prng.Next(0, 101);
                    AISkills.cooperation = prng.Next(0, 101);
                    AISkills.caution = prng.Next(0, 101);
                    AISkills.agility = prng.Next(0, 101);
                    AISkills.accuracy = prng.Next(0, 101);
                    newUnit.GetComponent<PurposeOfTravel>().purposeOfTravel = target.transform.position + new Vector3(0.68f,0,1.3f);
                    if (prng.Next(0, 5) == 0)
                    {
                        //newUnit.GetComponent<Flamethrower>().enabled = true;
                    }
                    if (prng.Next(0, 16) == 0)
                    {
                        newUnit.GetComponent<BlackHole>().enabled = true;
                    }
                    if (prng.Next(0, 3) == 0)
                    {
                        newUnit.GetComponent<ThunderStrike>().enabled = true;
                    }
                    if (prng.Next(0, 1) == 0)
                    {
                        newUnit.GetComponent<HeavyStrike>().enabled = true;
                    }
                    if (prng.Next(0, 3) == 0)
                    {
                        newUnit.GetComponent<MeleeAttack>().enabled = true;
                    }
                }
            }
        }

        if (has == false) {
            GameObject newUnit = Instantiate(unit, transform.position, transform.rotation);
            newUnit.GetComponent<UnitTeam>().name = ourTeam;
            //newUnit.GetComponent<UnitTeam>().FindOurTeam();
            newUnit.GetComponent<MoveSpeed>().moveSpeed = Random.Range(4, 10);
            AISkills AISkills = newUnit.GetComponent<AISkills>();
            AISkills.aggression = Random.Range(0, 100);
            AISkills.cooperation = Random.Range(0, 100);
            AISkills.caution = Random.Range(0, 100);
            AISkills.agility = Random.Range(0, 100);
            AISkills.accuracy = Random.Range(0, 100);
            newUnit.GetComponent<PurposeOfTravel>().purposeOfTravel = target.transform.position;
            if (Random.Range(0, 2) == 0)
            {
                newUnit.GetComponent<AOESkill>().enabled = false;
            }
            if (Random.Range(0, 2) == 0)
            {
                newUnit.GetComponent<HeavyStrike>().enabled = false;
            }
            if (Random.Range(0, 2) == 0)
            {
                newUnit.GetComponent<MeleeAttack>().enabled = false;
            }
        }
    }
}
