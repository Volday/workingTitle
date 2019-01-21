using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Team
{
    public int number;
    public List<GameObject> units = new List<GameObject>();
    public Team(int number)
    {
        this.number = number;
    }
}
