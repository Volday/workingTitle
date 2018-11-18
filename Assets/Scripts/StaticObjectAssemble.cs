using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectAssemble
{
    System.Random prng;
    int seed;

    public StaticObjectAssemble(int seed) {
        this.seed = seed;
        prng = new System.Random(seed);
    }

    public Vector3[] BuildingAssemble(int[,] coverageMap, float[,] wholeMapData, Vector2[] roadJunctions, int numberOfBuildings, int minBuildSide, int maxBuildSide) {
        BuildGenerator buildGenerator = new BuildGenerator(seed);

        Vector3[] buildingAssemble = new Vector3[numberOfBuildings];

        roadJunctions = MyMath.MixVector2(roadJunctions, seed);

        //Генерация зданий

        return buildingAssemble;
    }
}
