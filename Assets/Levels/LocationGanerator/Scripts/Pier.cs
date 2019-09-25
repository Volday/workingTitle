using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pier
{
    public Vector2 location;
    public List<Pier> neighbors = new List<Pier>();
    public bool coastPoint = false;
    public Island island;
    public List<Vector2> platformPillars = new List<Vector2>();
    public float maxDistanceToBridge = 0;
}
