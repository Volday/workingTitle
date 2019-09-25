using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableChain
{
    public List<Pier> piers = new List<Pier>();
    public Island island = null;
    public List<WalkableChain> walkableChains = new List<WalkableChain>();
}
