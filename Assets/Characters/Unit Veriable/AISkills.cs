using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISkills : MonoBehaviour
{
    System.Random prng;
    [Range(0,100)]
    public float cooperation;
    [Range(0, 100)]
    public float accuracy;
    [Range(0, 100)]
    public float agility;
    [Range(0, 100)]
    public float aggression;
    [Range(0, 100)]
    public float caution;

    public int personalSeed = 0;

    private void Start()
    {
        prng = new System.Random(personalSeed);
    }

    //fisrt может выпасть last нет
    public int GetRandomNumber(int first, int last) {
        return prng.Next(first, last);
    }
}
