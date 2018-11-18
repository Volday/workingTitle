using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGenerator {

    System.Random prng;

    public BuildGenerator(int seed) {
        prng = new System.Random(seed);
    }

    public Vector2[] ListBuildGenerator(int quantityOfChanks, float minDistanceBetweenRoads, int buildSize)
    {
        Vector2[] listBuild = new Vector2[quantityOfChanks];

        for (int i = 0; i < listBuild.Length; i++) {
            listBuild[i] = OneBuildGenerate(10, buildSize);
        }

        return listBuild;
    }

    public Vector2 OneBuildGenerate (int minSide, int maxSide)
    {
        int randomSizeHeight = 0;
        int randomSizeWidth = 0;
        int randomSizesCount = 3;

        for (int i = 0; i < randomSizesCount; i++) {
            randomSizeHeight += prng.Next(minSide, maxSide);
        }
        randomSizeHeight = randomSizeHeight / randomSizesCount;

        for (int i = 0; i < randomSizesCount; i++)
        {
            randomSizeWidth += prng.Next(minSide, maxSide);
        }
        randomSizeWidth = randomSizeWidth / randomSizesCount;
        return new Vector2(randomSizeWidth, randomSizeHeight);
    }
}
