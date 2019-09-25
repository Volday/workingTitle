using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hydra.HydraCommon.Utils.Comparers;
using System;

public class StaticObjectAssemble
{
    System.Random prng;
    int seed;

    StaticObjectGenerator staticObjectGenerator;

    List<HouseStaticObject> houseStaticObjects = new List<HouseStaticObject>();

    int[,] islandsMap;
    List<IslandForPier> islandForPiers;
    int[,] waterMap;
    List<Vector2> waterPoint = new List<Vector2>();

    int[,] invisibleWallMap;
    List<IslandForPier> invisibleWallsLine = new List<IslandForPier>();

    int currentIslandSize;

    MapGenerator mapGenerator;

    public StaticObjectAssemble(int seed, StaticObjectGenerator staticObjectGenerator, MapGenerator mapGenerator) {
        this.seed = seed;
        prng = new System.Random(seed);
        this.staticObjectGenerator = staticObjectGenerator;
        this.mapGenerator = mapGenerator;
    }

    public void WaterAssemble(int chunkSize, float minFreeHeight, float meshHeightMultiplier, AnimationCurve meshHeightCurve, int widthLocation, int heightLocation)
    {
        chunkSize = chunkSize - 1;
        Vector2 coord = new Vector2(((widthLocation * (float)chunkSize))  / 2 - (float)chunkSize / 2, 
            ((heightLocation * (float)chunkSize)) / 2 - (float)chunkSize / 2);
        float height = meshHeightCurve.Evaluate(minFreeHeight) * meshHeightMultiplier;
        staticObjectGenerator.WaterGenerate(coord, widthLocation + 2, heightLocation + 2, height);
    }

    public void InvisibleWallAssemble(float[,] wholeMapData, float maxFreeHeight, float meshHeightMultiplier)
    {
        maxFreeHeight = maxFreeHeight + 0.07f;

        islandForPiers = new List<IslandForPier>();

        islandsMap = new int[wholeMapData.GetLength(0), wholeMapData.GetLength(1)];

        for (int y = 8; y < wholeMapData.GetLength(1) - 7; y++) {
            for (int x = 8; x < wholeMapData.GetLength(0) - 7; x++)
            {
                if (islandsMap[x, y] == 0 && wholeMapData[x, y] > maxFreeHeight) {
                    IslandForPier newIslandForPier = new IslandForPier();
                    FindFullIsland(new Vector2(x, y), newIslandForPier, wholeMapData, maxFreeHeight);

                    for (int i = 0; i < newIslandForPier.coastline.Count; i++)
                    {
                        bool isLine = false;
                        if (islandsMap[(int)newIslandForPier.coastline[i].x - 1, (int)newIslandForPier.coastline[i].y] == 1) {
                            isLine = true;
                        }
                        if (islandsMap[(int)newIslandForPier.coastline[i].x + 1, (int)newIslandForPier.coastline[i].y] == 1)
                        {
                            isLine = true;
                        }
                        if (islandsMap[(int)newIslandForPier.coastline[i].x, (int)newIslandForPier.coastline[i].y - 1] == 1)
                        {
                            isLine = true;
                        }
                        if (islandsMap[(int)newIslandForPier.coastline[i].x, (int)newIslandForPier.coastline[i].y + 1] == 1)
                        {
                            isLine = true;
                        }
                        if (!isLine) {
                            //islandsMap[(int)newIslandForPier.coastline[i].x, (int)newIslandForPier.coastline[i].y] = 3;
                            newIslandForPier.coastline.RemoveAt(i);
                            i--;
                        }
                    }

                    for (int i = 0; i < newIslandForPier.coastline.Count; i++)
                    {
                        islandsMap[(int)newIslandForPier.coastline[i].x, (int)newIslandForPier.coastline[i].y] = 2;
                    }

                    if (newIslandForPier.coastline.Count > 3) {
                        islandForPiers.Add(newIslandForPier);
                    }
                }
            }
        }

        List<Vector2> invisibleWallsPoints = new List<Vector2>();
        for (int t = 0; t < islandForPiers.Count; t++) {
            for (int i = 0; i < islandForPiers[t].coastline.Count; i++)
            {
                invisibleWallsPoints.Add(islandForPiers[t].coastline[i]);
            }
        }

        staticObjectGenerator.InvisibleWallGenerator(invisibleWallsPoints, wholeMapData.GetLength(0), wholeMapData.GetLength(1));
    }

    public int[,] PierAssemble(int[,] coverageMap, float[,] wholeMapData, float minFreeHeight, float maxFreeHeight, 
        int minIslandSize, int pierOffset, float maxPierDistance, float widthOfPiers, float minPierAngle) {
        float coastHeight = minFreeHeight - 0.1f;

        List<Island> islands = new List<Island>();

        islandForPiers = new List<IslandForPier>();

        islandsMap = new int[coverageMap.GetLength(0), coverageMap.GetLength(1)];
        waterMap = new int[coverageMap.GetLength(0), coverageMap.GetLength(1)];
        for (int y = 8; y < islandsMap.GetLength(1) - 7; y++) {
            for (int x = 8; x < islandsMap.GetLength(0) - 7; x++)
            {
                if (islandsMap[x, y] == 0 && wholeMapData[x, y] > coastHeight)
                {
                    currentIslandSize = 0;
                    IslandForPier newIslandForPier = new IslandForPier();
                    FindFullIsland(new Vector2(x, y), newIslandForPier, wholeMapData, coastHeight);

                    for (int i = 0; i < newIslandForPier.coastline.Count; i++)
                    {
                        bool isLine = false;
                        if (islandsMap[(int)newIslandForPier.coastline[i].x - 1, (int)newIslandForPier.coastline[i].y] == 1)
                        {
                            isLine = true;
                        }
                        if (islandsMap[(int)newIslandForPier.coastline[i].x + 1, (int)newIslandForPier.coastline[i].y] == 1)
                        {
                            isLine = true;
                        }
                        if (islandsMap[(int)newIslandForPier.coastline[i].x, (int)newIslandForPier.coastline[i].y - 1] == 1)
                        {
                            isLine = true;
                        }
                        if (islandsMap[(int)newIslandForPier.coastline[i].x, (int)newIslandForPier.coastline[i].y + 1] == 1)
                        {
                            isLine = true;
                        }
                        if (!isLine)
                        {
                            //islandsMap[(int)newIslandForPier.coastline[i].x, (int)newIslandForPier.coastline[i].y] = 3;
                            newIslandForPier.coastline.RemoveAt(i);
                            i--;
                        }
                    }

                    if (currentIslandSize > minIslandSize)
                    {
                        islandForPiers.Add(newIslandForPier);
                        Island newIsland = new Island();
                        newIsland.coastLine = newIslandForPier.coastline;
                        newIsland.size = currentIslandSize;
                        islands.Add(newIsland);
                    }

                    for (int i = 0; i < newIslandForPier.coastline.Count; i++)
                    {
                        islandsMap[(int)newIslandForPier.coastline[i].x, (int)newIslandForPier.coastline[i].y] = 2;
                    }
                }
            }
        }

        for (int y = 8; y < islandsMap.GetLength(1) - 7; y++)
        {
            for (int x = 8; x < islandsMap.GetLength(0) - 7; x++)
            {
                if (islandsMap[x, y] != 0)
                {
                    waterMap[x, y] = 1;
                }
            }
        }

        for (int t = 0; t < pierOffset; t++)
        {
            for (int y = 8; y < waterMap.GetLength(1) - 7; y++)
            {
                for (int x = 8; x < waterMap.GetLength(0) - 7; x++)
                {
                    if (waterMap[x - 1, y] == t + 1 && waterMap[x, y] == 0)
                    {
                        waterMap[x, y] = t + 2;
                        continue;
                    }
                    if (waterMap[x + 1, y] == t + 1 && waterMap[x, y] == 0)
                    {
                        waterMap[x, y] = t + 2;
                        continue;
                    }
                    if (waterMap[x, y - 1] == t + 1 && waterMap[x, y] == 0)
                    {
                        waterMap[x, y] = t + 2;
                        continue;
                    }
                    if (waterMap[x, y + 1] == t + 1 && waterMap[x, y] == 0)
                    {
                        waterMap[x, y] = t + 2;
                        continue;
                    }
                }
            }
        }

        for (int y = 12; y < waterMap.GetLength(1) - 12; y++)
        {
            for (int x = 12; x < waterMap.GetLength(0) - 12; x++)
            {
                if (waterMap[x, y] == 0)
                {
                    waterPoint.Add(new Vector2(x, y));
                }
            }
        }

        List<Pier> piers = new List<Pier>();

        while (waterPoint.Count > 0) {
            int nextIndex = prng.Next(0, waterPoint.Count);
            Vector2 nextWaterPoint = waterPoint[nextIndex];

            Pier pier = new Pier();
            pier.location = nextWaterPoint;
            piers.Add(pier);

            waterMap[(int)nextWaterPoint.x, (int)nextWaterPoint.y] = 200;

            IslandForPier nextIslandForPier = new IslandForPier();
            nextIslandForPier.coastline.Add(nextWaterPoint);
            CloseFreeWater(pierOffset, nextIslandForPier, waterPoint);

            waterPoint.Clear();
            for (int y = 12; y < waterMap.GetLength(1) - 12; y++)
            {
                for (int x = 12; x < waterMap.GetLength(0) - 12; x++)
                {
                    if (waterMap[x, y] == 0)
                    {
                        waterPoint.Add(new Vector2(x, y));
                    }
                }
            }
        }

        //x first point, y second point, z distance between
        List<Vector3> pairOfPiers = new List<Vector3>();
        List<Vector2> doublePairOfPiers = new List<Vector2>();
        float sqrMaxPierDistance = maxPierDistance * maxPierDistance;
        for (int t = 0; t < piers.Count; t++) {
            for (int i = 0; i < piers.Count; i++)
            {
                if (t != i) {
                    if (!doublePairOfPiers.Contains(new Vector2(i, t)))
                    {
                        float sqrDistance = MyMath.sqrDistanceFromPointToPoint(piers[t].location, piers[i].location);
                        if (sqrDistance < sqrMaxPierDistance) {
                            if (pairOfPiers.Count == 0)
                            {
                                pairOfPiers.Add(new Vector3(t, i, sqrDistance));
                                doublePairOfPiers.Add(new Vector2(t, i));
                            }
                            else
                            {
                                if (pairOfPiers[pairOfPiers.Count - 1].z > sqrDistance)
                                {
                                    for (int j = 0; j < pairOfPiers.Count; j++)
                                    {
                                        if (pairOfPiers[j].z > sqrDistance)
                                        {
                                            pairOfPiers.Add(new Vector3());
                                            for (int k = pairOfPiers.Count - 1; k > j; k--)
                                            {
                                                Vector3 nextVector3 = new Vector3();
                                                nextVector3.x = pairOfPiers[k - 1].x;
                                                nextVector3.y = pairOfPiers[k - 1].y;
                                                nextVector3.z = pairOfPiers[k - 1].z;
                                                pairOfPiers[k] = nextVector3;
                                            }
                                            pairOfPiers[j] = new Vector3(t, i, sqrDistance);
                                            doublePairOfPiers.Add(new Vector2(t, i));
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    pairOfPiers.Add(new Vector3(t, i, sqrDistance));
                                    doublePairOfPiers.Add(new Vector2(t, i));
                                }
                            }
                        }
                    }
                    else {
                        doublePairOfPiers.Remove(new Vector2(i, t));
                    }
                }
            }
        }

        for (int t = 0; t < pairOfPiers.Count; t++) {
            if (CheckPierCompound(piers[(int)pairOfPiers[t].x], piers[(int)pairOfPiers[t].y], minPierAngle))
            {
                piers[(int)pairOfPiers[t].x].neighbors.Add(piers[(int)pairOfPiers[t].y]);
                piers[(int)pairOfPiers[t].y].neighbors.Add(piers[(int)pairOfPiers[t].x]);
            }
        }

        List<WalkableChain> walkableChains = new List<WalkableChain>();
        for (int t = 0; t < piers.Count; t++) {
            bool alreadyCheckThisPier = false;

            for (int i = 0; i < walkableChains.Count; i++) {
                if (walkableChains[i].piers.Contains(piers[t])) {
                    alreadyCheckThisPier = true;
                    break;
                }
            }
            if (!alreadyCheckThisPier) {
                WalkableChain newWalkableChain = new WalkableChain();
                newWalkableChain.piers.Add(piers[t]);
                CreateWallkableChain(piers[t], newWalkableChain);
                walkableChains.Add(newWalkableChain);
            }
        }

        //Add islands to walkableChains
        for (int t = 0; t < islands.Count; t++)
        {
            WalkableChain newWalkableChain = new WalkableChain();
            newWalkableChain.island = islands[t];
            walkableChains.Add(newWalkableChain);
        }

        //Connecting islands with Pirs
        for (int t = 0; t < islands.Count; t++)
        {
            List<Vector2> resultsCoord = new List<Vector2>();
            List<Pier> resultsPier = new List<Pier>();
            List<float> sqrResultsDistance = new List<float>();

            for (int j = 0; j < walkableChains.Count - islands.Count; j++)
            {
                resultsCoord.Add(Vector2.zero);
                resultsPier.Add(new Pier());
                sqrResultsDistance.Add(float.MaxValue);
            }

            //finding closest points
            for (int i = 0; i < islands[t].coastLine.Count; i++)
            {
                for (int j = 0; j < walkableChains.Count - islands.Count; j++)
                {
                    for (int k = 0; k < walkableChains[j].piers.Count; k++)
                    {
                        float newSqrResultsDistance = MyMath.sqrDistanceFromPointToPoint(walkableChains[j].piers[k].location,
                            islands[t].coastLine[i]);
                        if (newSqrResultsDistance < sqrResultsDistance[j])
                        {
                            resultsCoord[j] = islands[t].coastLine[i];
                            resultsPier[j] = walkableChains[j].piers[k];
                            sqrResultsDistance[j] = newSqrResultsDistance;
                        }
                    }
                }
            }

            //making the connect
            for (int j = 0; j < walkableChains.Count - islands.Count; j++)
            {
                if (sqrResultsDistance[j] < maxPierDistance * maxPierDistance)
                {
                    Pier newPier = new Pier();
                    newPier.location = resultsCoord[j];
                    newPier.neighbors.Add(resultsPier[j]);
                    resultsPier[j].neighbors.Add(newPier);
                    newPier.coastPoint = true;
                    newPier.island = islands[t];
                    walkableChains[j].piers.Add(newPier);
                    walkableChains[j].walkableChains.Add(walkableChains[walkableChains.Count - islands.Count + t]);
                    walkableChains[walkableChains.Count - islands.Count + t].walkableChains.Add(walkableChains[j]);
                }
            }
        }

        walkableChains = FindBiggestWalkableChains(walkableChains);

        coverageMap = DrawOnCoverageMap(coverageMap, walkableChains, widthOfPiers);

        for (int t = 0; t < walkableChains.Count; t++) {
            if (walkableChains[t].piers.Count > 0) {
                for (int i = 0; i < walkableChains[t].piers.Count; i++) {
                    CreatePierPlatform(walkableChains[t].piers[i]);
                }
            }
        }

        Vector2[] ar = new Vector2[4];
        ar[2] = new Vector2(0, 1);
        ar[3] = new Vector2(1, 0);
        ar[0] = new Vector2(-1, 0);
        ar[1] = new Vector2(0, -1);
        Array.Sort(ar, new ClockwiseComparer(Vector2.zero));
        Debug.Log("test start");
        for (int t = 0; t < ar.Length; t++) {
            Debug.Log(ar[t]);
        }
        Debug.Log("test end");

        return coverageMap;
    }

    void CreatePierPlatform(Pier pier) {

    }

    int[,] DrawOnCoverageMap(int[,] coverageMap, List<WalkableChain> walkableChains, float widthOfPiers) {
        List<Vector4> pierLines = new List<Vector4>();
        for (int t = 0; t < walkableChains.Count; t++) {
            if (walkableChains[t].piers.Count > 0) {
                for (int i = 0; i < walkableChains[t].piers.Count; i++) {
                    if (walkableChains[t].piers[i].neighbors.Count > 0)
                    {
                        for (int j = 0; j < walkableChains[t].piers[i].neighbors.Count; j++)
                        {
                            pierLines.Add(new Vector4(walkableChains[t].piers[i].location.x, walkableChains[t].piers[i].location.y,
                                walkableChains[t].piers[i].neighbors[j].location.x, walkableChains[t].piers[i].neighbors[j].location.y));
                        }
                    }
                }
            }
        }

        for (int y = 0; y < coverageMap.GetLength(1); y++)
        {
            for (int x = 0; x < coverageMap.GetLength(0); x++)
            {
                for (int t = 0; t < pierLines.Count; t++)
                {
                    if (MyMath.sqrDistanceFromPointToSection(new Vector2(x, y), new Vector2(pierLines[t].x, pierLines[t].y),
                        new Vector2(pierLines[t].z, pierLines[t].w)) < widthOfPiers * widthOfPiers / 4)
                    {
                        coverageMap[x, y] = 5;
                    }
                }
            }
        }

        return coverageMap;
    }

    List<WalkableChain> FindBiggestWalkableChains(List<WalkableChain> walkableChains) {
        List<WalkableChain> mainWalkableChain = new List<WalkableChain>();
        for (int t = 0; t < walkableChains.Count; ) {
            List<WalkableChain> newMainWalkableChain = new List<WalkableChain>();
            WalkableChain nextWalkableChain = walkableChains[0];
            newMainWalkableChain.Add(nextWalkableChain);
            walkableChains.Remove(nextWalkableChain);
            FindWalkableChains(nextWalkableChain, newMainWalkableChain, walkableChains);
            if (newMainWalkableChain.Count > mainWalkableChain.Count) {
                mainWalkableChain = newMainWalkableChain;
            }
        }
        return mainWalkableChain;
    }

    void FindWalkableChains(WalkableChain walkableChain, List<WalkableChain> newMainWalkableChain, List<WalkableChain> walkableChains) {
        for (int t = 0; t < walkableChain.walkableChains.Count; t++) {
            if (walkableChains.Contains(walkableChain.walkableChains[t])) {
                WalkableChain nextWalkableChain = walkableChain.walkableChains[t];
                newMainWalkableChain.Add(nextWalkableChain);
                walkableChains.Remove(nextWalkableChain);
                FindWalkableChains(nextWalkableChain, newMainWalkableChain, walkableChains);
            }
        }
    }

    void CreateWallkableChain(Pier pier, WalkableChain walkableChain) {
        for (int t = 0; t < pier.neighbors.Count; t++) {
            if (!walkableChain.piers.Contains(pier.neighbors[t])) {
                walkableChain.piers.Add(pier.neighbors[t]);
                CreateWallkableChain(pier.neighbors[t], walkableChain);
            }
        }
    }

    bool CheckPierCompound(Pier pierA, Pier pierB, float minPierAngle) {
        bool result = true;
        if (pierA.neighbors.Count > 0 || pierB.neighbors.Count > 0) {
            for (int t = 0; t < pierA.neighbors.Count; t++)
            {
                if (Vector3.Angle(pierA.neighbors[t].location - pierA.location, pierB.location - pierA.location) < minPierAngle)
                {
                    result = false;
                    break;
                }
            }
            if (result)
            {
                for (int t = 0; t < pierB.neighbors.Count; t++)
                {
                    if (Vector3.Angle(pierB.neighbors[t].location - pierB.location, pierA.location - pierB.location) < minPierAngle)
                    {
                        result = false;
                        break;
                    }
                }
            }
        }

        return result;
    }

    void CloseFreeWater(int pierOffset, IslandForPier newIslandForPier, List<Vector2> waterPoint) {
        List<Vector2> hasntCheckCoastLinePoints = newIslandForPier.coastline;
        for (int i = 0; i < hasntCheckCoastLinePoints.Count; i++)
        {
            for (int y = -pierOffset; y < pierOffset; y++)
            {
                for (int x = -pierOffset; x < pierOffset; x++)
                {
                    Vector2 nextPoint = hasntCheckCoastLinePoints[i];
                    nextPoint.x += x;
                    nextPoint.y += y;
                    if (nextPoint.x >= 0 && nextPoint.y >= 0 &&
                        nextPoint.x < waterMap.GetLength(0) && nextPoint.y < waterMap.GetLength(1))
                    {
                        if (waterMap[(int)nextPoint.x, (int)nextPoint.y] == 0)
                        {
                            if (MyMath.sqrDistanceFromPointToPoint(nextPoint, hasntCheckCoastLinePoints[i]) < pierOffset * pierOffset)
                            {
                                waterMap[(int)nextPoint.x, (int)nextPoint.y] = 1;
                            }
                        }
                    }
                }
            }
        }
    }

    void FindFullIsland(Vector2 coord, IslandForPier newIslandForPier, float[,] wholeMapData, float coastHeight) {
        Queue<Vector2> coords = new Queue<Vector2>();
        coords.Enqueue(coord);
        islandsMap[(int)coord.x, (int)coord.y] = 1;
        while (coords.Count > 0) {
            if (coords.Count > 1000000) {
                break;
            }
            currentIslandSize++;
            bool isPartOfCoastline = false;
            Vector2 currentCoord = coords.Dequeue();
            if (currentCoord.x > 8 && currentCoord.y > 8 && currentCoord.x < islandsMap.GetLength(0) - 7 && currentCoord.y < islandsMap.GetLength(1) - 7) {
                if (islandsMap[(int)currentCoord.x - 1, (int)currentCoord.y] == 0)
                {
                    if (wholeMapData[(int)currentCoord.x - 1, (int)currentCoord.y] > coastHeight) {
                        islandsMap[(int)currentCoord.x - 1, (int)currentCoord.y] = 1;
                        coords.Enqueue(new Vector2(currentCoord.x - 1, currentCoord.y));
                    }
                    else
                    {
                        isPartOfCoastline = true;
                    }
                }


                if (islandsMap[(int)currentCoord.x + 1, (int)currentCoord.y] == 0)
                {
                    if (wholeMapData[(int)currentCoord.x + 1, (int)currentCoord.y] > coastHeight)
                    {
                        islandsMap[(int)currentCoord.x + 1, (int)currentCoord.y] = 1;
                        coords.Enqueue(new Vector2(currentCoord.x + 1, currentCoord.y));
                    }
                    else
                    {
                        isPartOfCoastline = true;
                    }
                }

                if (islandsMap[(int)currentCoord.x, (int)currentCoord.y - 1] == 0)
                {
                    if (wholeMapData[(int)currentCoord.x, (int)currentCoord.y - 1] > coastHeight)
                    {
                        islandsMap[(int)currentCoord.x, (int)currentCoord.y - 1] = 1;
                        coords.Enqueue(new Vector2(currentCoord.x, currentCoord.y - 1));
                    }
                    else
                    {
                        isPartOfCoastline = true;
                    }
                }

                if (islandsMap[(int)currentCoord.x, (int)currentCoord.y + 1] == 0)
                {
                    if (wholeMapData[(int)currentCoord.x, (int)currentCoord.y + 1] > coastHeight)
                    {
                        islandsMap[(int)currentCoord.x, (int)currentCoord.y + 1] = 1;
                        coords.Enqueue(new Vector2(currentCoord.x, currentCoord.y + 1));
                    }
                    else
                    {
                        isPartOfCoastline = true;
                    }
                }
            }
            if (isPartOfCoastline)
            {
                newIslandForPier.coastline.Add(currentCoord);
            }
        }
    }

    public int[,] TreeAssemble(int[,] coverageMap, float[,] wholeMapData, float minFreeHeight, float maxFreeHeight, int treeCount) {

        Vector2[] trees = new Vector2[(coverageMap.GetLength(1)) * (coverageMap.GetLength(0))];
        for (int y = 0; y < coverageMap.GetLength(1) - 1; y++) {
            for (int x = 0; x < coverageMap.GetLength(0) - 1; x++) {
                trees[y * coverageMap.GetLength(0) + x] = new Vector2(x, y);
            }
        }

        trees = MyMath.MixVector2(trees, seed);
        List<Vector2> treesPosition = new List<Vector2>();

        int treesCurrentCount = 0;
        for (int i = 0; i < trees.Length; i++) {
            if (treesCurrentCount < treeCount) {
                if (wholeMapData[(int)trees[i].x, (int)trees[i].y] > minFreeHeight && wholeMapData[(int)trees[i].x, (int)trees[i].y] < maxFreeHeight && coverageMap[(int)trees[i].x, (int)trees[i].y] == 0) {
                    treesCurrentCount++;
                    treesPosition.Add(new Vector2(trees[i].x, trees[i].y));
                    coverageMap[(int)trees[i].x, (int)trees[i].y] = 4;
                }
            }
        }

        for (int i = 0; i < treesPosition.Count; i++)
        {
            float height = mapGenerator.meshHeightCurve.Evaluate(wholeMapData[(int)treesPosition[i].x, (int)treesPosition[i].y]) * mapGenerator.meshHeightMultiplier;
            staticObjectGenerator.TreeGenerate(treesPosition[i], seed, coverageMap.GetLength(0), coverageMap.GetLength(1), height);
        }

        return coverageMap;
    }

    public int[,] BuildingAssemble(int[,] coverageMap, float[,] wholeMapData, RoadGenerator roadGenerator, int numberOfBuildings, int minBuildSide, int maxBuildSide, float widthRoad, float minFreeHeight, float maxFreeHeight, float closedAreaHouse) {

        Vector2[] roadJunctions = roadGenerator.GetRoadJunctions();
        roadJunctions = MyMath.MixVector2(roadJunctions, seed);

        //Генерация зданий
        int currentNumberOfBuildings = 0;
        for (int i = 0; i < roadJunctions.Length; i++) {
            if (currentNumberOfBuildings < numberOfBuildings) {

                for (int t = 0; t < 360; t = t + 180) {
                    if (roadGenerator.GetRoadJunctionsNeighbor(roadJunctions[i], 0).x != -1 &&
                        roadGenerator.GetRoadJunctionsNeighbor(roadJunctions[i], 2).x != -1 &&
                        roadGenerator.GetRoadJunctionsNeighbor(roadJunctions[i], 1).x == -1 &&
                        roadGenerator.GetRoadJunctionsNeighbor(roadJunctions[i], 3).x == -1) {

                        int searchRange = (int)Mathf.Sqrt((maxBuildSide + widthRoad * 3) * (maxBuildSide + widthRoad * 3) * 2) + 1;

                        Vector2 offset = MyMath.Rotate(((roadGenerator.GetRoadJunctionsNeighbor(roadJunctions[i], 0) - roadJunctions[i])).normalized, t + 90);
                        float offsetAngle;
                        if (offset.x < 0)
                        {
                            offsetAngle = (Mathf.Acos(offset.x * 0 + offset.y * 1) / Mathf.PI) * 180.0f;
                        }
                        else {
                            offsetAngle = 360.0f - (Mathf.Acos(offset.x * 0 + offset.y * 1) / Mathf.PI) * 180.0f;
                        }

                        Vector2 zonePoint1 = MyMath.Rotate(new Vector2(-maxBuildSide / 2, widthRoad), offsetAngle);
                        Vector2 zonePoint2 = MyMath.Rotate(new Vector2(-maxBuildSide / 2, widthRoad + maxBuildSide), offsetAngle);
                        Vector2 zonePoint3 = MyMath.Rotate(new Vector2(maxBuildSide / 2, widthRoad + maxBuildSide), offsetAngle);
                        Vector2 zonePoint4 = MyMath.Rotate(new Vector2(maxBuildSide / 2, widthRoad), offsetAngle);

                        int yStart = -searchRange;
                        int yFinish = searchRange;
                        int xStart = -searchRange;
                        int xFinish = searchRange;
                        bool swapCoord = false;
                        int incrementX = 1;
                        int incrementY = 1;
                        if (offsetAngle <= 45 || offsetAngle >= 315)
                        {
                            yStart = searchRange;
                            yFinish = -searchRange;
                            incrementY = -1;
                            swapCoord = true;
                        }
                        else if (offsetAngle >= 135 && offsetAngle <= 225)
                        {
                            swapCoord = true;
                        }
                        else if (offsetAngle >= 225 && offsetAngle <= 315)
                        {
                            xStart = searchRange;
                            xFinish = -searchRange;
                            incrementX = -1;
                        }

                        bool hasHouse = true;

                        for (int yy = yStart; yy != yFinish; yy += incrementY)
                        {
                            for (int xx = xStart; xx != xFinish; xx += incrementX)
                            {
                                int y = yy;
                                int x = xx;
                                if (swapCoord)
                                {
                                    y = xx;
                                    x = yy;
                                }

                                if (x + (int)roadJunctions[i].x < 0 || y + (int)roadJunctions[i].y < 0 ||
                                    x + (int)roadJunctions[i].x >= coverageMap.GetLength(0) || y + (int)roadJunctions[i].y >= coverageMap.GetLength(1))
                                {
                                    hasHouse = false;
                                    break;
                                }

                                if (MyMath.PointBelongsTriangle(new Vector2(x, y), zonePoint1, zonePoint2, zonePoint3) ||
                                    MyMath.PointBelongsTriangle(new Vector2(x, y), zonePoint3, zonePoint4, zonePoint1))
                                {
                                    if (x + (int)roadJunctions[i].x >= 0 && y + (int)roadJunctions[i].y >= 0 && x + (int)roadJunctions[i].x < coverageMap.GetLength(0) && y + (int)roadJunctions[i].y < coverageMap.GetLength(1)) {
                                        if (!(coverageMap[x + (int)roadJunctions[i].x, y + (int)roadJunctions[i].y] == 0 &&
                                            wholeMapData[x + (int)roadJunctions[i].x, y + (int)roadJunctions[i].y] > minFreeHeight - closedAreaHouse &&
                                            wholeMapData[x + (int)roadJunctions[i].x, y + (int)roadJunctions[i].y] < maxFreeHeight + closedAreaHouse))
                                        {
                                            //ab = 12 bc = 23 cd = 34 da = 41
                                            float AB = MyMath.sqrDistanceFromPointToSection(new Vector2(x, y), zonePoint1, zonePoint2);
                                            float BC = MyMath.sqrDistanceFromPointToSection(new Vector2(x, y), zonePoint2, zonePoint3);
                                            float CD = MyMath.sqrDistanceFromPointToSection(new Vector2(x, y), zonePoint3, zonePoint4);
                                            float DA = MyMath.sqrDistanceFromPointToSection(new Vector2(x, y), zonePoint4, zonePoint1);

                                            if (AB <= BC && AB <= CD && AB <= DA)
                                            {
                                                AB = Mathf.Sqrt(AB);
                                                zonePoint1 += MyMath.Rotate(new Vector2(1, 0), offsetAngle) * AB;
                                                zonePoint2 += MyMath.Rotate(new Vector2(1, 0), offsetAngle) * AB;
                                            }
                                            else if (BC <= AB && BC <= CD && BC <= DA)
                                            {
                                                BC = Mathf.Sqrt(BC);
                                                zonePoint2 += MyMath.Rotate(new Vector2(0, -1), offsetAngle) * BC;
                                                zonePoint3 += MyMath.Rotate(new Vector2(0, -1), offsetAngle) * BC;
                                            }
                                            else if (CD <= AB && CD <= BC && CD <= DA)
                                            {
                                                CD = Mathf.Sqrt(CD);
                                                zonePoint3 += MyMath.Rotate(new Vector2(-1, 0), offsetAngle) * CD;
                                                zonePoint4 += MyMath.Rotate(new Vector2(-1, 0), offsetAngle) * CD;
                                            }
                                            else if (DA <= AB && DA <= BC && DA <= CD)
                                            {
                                                DA = Mathf.Sqrt(DA);
                                                zonePoint4 += MyMath.Rotate(new Vector2(0, 1), offsetAngle) * DA;
                                                zonePoint1 += MyMath.Rotate(new Vector2(0, 1), offsetAngle) * DA;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        Vector2 preWidthHouse = zonePoint1 - zonePoint2;
                        float widthHouse = preWidthHouse.x * preWidthHouse.x + preWidthHouse.y * preWidthHouse.y;
                        Vector2 preHeightHouse = zonePoint1 - zonePoint4;
                        float heightHouse = preHeightHouse.x * preHeightHouse.x + preHeightHouse.y * preHeightHouse.y;
                        Vector2 preDoor = zonePoint1 + zonePoint4;
                        if (widthHouse >= minBuildSide * minBuildSide && heightHouse >= minBuildSide * minBuildSide &&
                            widthHouse <= maxBuildSide * maxBuildSide && heightHouse <= maxBuildSide * maxBuildSide &&
                            /*(preDoor.x * preDoor.x) + (preDoor.y * preDoor.y) < (widthRoad * widthRoad * 30)*/
                            MyMath.sqrDistanceFromPointToSection(new Vector2(0, 0), zonePoint1, zonePoint4) < (widthRoad * widthRoad * 2) && hasHouse)
                        {
                            Vector2 location = (zonePoint1 + zonePoint2 + zonePoint3 + zonePoint4) / 4 + roadJunctions[i];

                            houseStaticObjects.Add(new HouseStaticObject(location, new Vector2(Mathf.Sqrt(widthHouse), Mathf.Sqrt(heightHouse)), offsetAngle));

                            for (int y = -searchRange; y < searchRange; y++)
                            {
                                for (int x = -searchRange; x < searchRange; x++)
                                {
                                    if (MyMath.PointBelongsTriangle(new Vector2(x, y), zonePoint1, zonePoint2, zonePoint3) ||
                                    MyMath.PointBelongsTriangle(new Vector2(x, y), zonePoint3, zonePoint4, zonePoint1))
                                    {
                                        coverageMap[x + (int)roadJunctions[i].x, y + (int)roadJunctions[i].y] = 3;
                                    }
                                }
                            }
                            currentNumberOfBuildings++;
                        }
                    }
                }
            }
            else {
                break;
            }
        }

        for (int i = 0; i < houseStaticObjects.Count; i++) {
            staticObjectGenerator.OneBuildGenerate(houseStaticObjects[i].location, houseStaticObjects[i].dimension, houseStaticObjects[i].angle, seed, coverageMap.GetLength(0), coverageMap.GetLength(1));
        }

        return coverageMap;
    }

    class IslandForPier {
        public List<Vector2> coastline;
        public IslandForPier() {
            coastline = new List<Vector2>();
        }
    }

    class HouseStaticObject {
        public Vector2 location;
        public Vector2 dimension;
        public float angle;

        public HouseStaticObject(Vector2 location, Vector2 dimension, float angle) {
            this.location = location;
            this.dimension = dimension;
            this.angle = angle;
        }
    }
}
