using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Team
{
    public string name;
    public List<GameObject> units = new List<GameObject>();
    public Team(string name) {
        this.name = name;
    }
}
