using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator {

    System.Random prng;
    float[,] wholeMapData;
    int[,] coverageMap;
    float minFreeHeight;
    float maxFreeHeight;
    float angle;
    float stapLenght;
    float widthRoad;
    float minDistanceBetweenRoads;
    int minRoadLenght;
    float closedArea;

    int alreadyFindStartPoint;

    int maxDeep;
    int currentMaxDeep;

    Vector2 startPoint;
    Vector2 finishPoint;

    RoadJunction bestStartRoadJunction;
    RoadJunction bestCurrentStartRoadJunction;
    RoadJunction bestFinishRoadJunction;
    RoadJunction bestCurrentRoadJunction;

    List<RoadJunction> neighborsRoadJunctions = new List<RoadJunction>();

    List<RoadJunction> roadJunctions = new List<RoadJunction>();

    List<RoadJunction> singleRoadJunctions = new List<RoadJunction>();

    public RoadGenerator(float[,] wholeMapData, int[,] coverageMap, float minFreeHeight, float maxFreeHeight, float angle, float stapLenght, float widthRoad, int seed, float minDistanceBetweenRoads, int minRoadLenght, float closedArea) {
        prng = new System.Random(seed);
        this.wholeMapData = wholeMapData;
        this.coverageMap = coverageMap;
        this.minFreeHeight = minFreeHeight;
        this.maxFreeHeight = maxFreeHeight;
        this.angle = angle;
        this.stapLenght = stapLenght;
        this.widthRoad = widthRoad;
        this.minDistanceBetweenRoads = minDistanceBetweenRoads;
        this.minRoadLenght = minRoadLenght;
        this.closedArea = closedArea;

        for (int y = 0; y < coverageMap.GetLength(1); y++)
        {
            for (int x = 0; x < coverageMap.GetLength(0); x++)
            {
                if (wholeMapData[x, y] > (minFreeHeight - closedArea) && wholeMapData[x, y] < (maxFreeHeight - closedArea))
                {
                    coverageMap[x, y] = 0; //свободно
                }
                else
                {
                    coverageMap[x, y] = 1; //занято ландшафтом
                }
            }
        }
    }

    public int[,] RoadGenerateFromNoise()
    {
        while (true)
        {
            bool roadJunctionsEmpty = true;
            int branchLenght = roadJunctions.Count;
            for (int i = 0; i < branchLenght; i++)
            {
                if (roadJunctions[i].neighbors[0] == null)
                {
                    roadJunctionsEmpty = false;
                    GenerateRoadBranch(roadJunctions[i], 0, minRoadLenght, roadJunctions[i], 0);
                }
                else if (roadJunctions[i].neighbors[1] == null)
                {
                    roadJunctionsEmpty = false;
                    GenerateRoadBranch(roadJunctions[i], 1, minRoadLenght, roadJunctions[i], 1);
                }
                else if (roadJunctions[i].neighbors[2] == null)
                {
                    roadJunctionsEmpty = false;
                    GenerateRoadBranch(roadJunctions[i], 2, minRoadLenght, roadJunctions[i], 2);
                }
                else if (roadJunctions[i].neighbors[3] == null)
                {
                    roadJunctionsEmpty = false;
                    GenerateRoadBranch(roadJunctions[i], 3, minRoadLenght, roadJunctions[i], 3);
                }
                else if (roadJunctions[i].neighbors[0].location.x == -1 && roadJunctions[i].neighbors[1].location.x == -1 && roadJunctions[i].neighbors[2].location.x == -1 && roadJunctions[i].neighbors[3].location.x == -1)
                {
                    if (singleRoadJunctions.IndexOf(roadJunctions[i]) == -1) {
                        singleRoadJunctions.Add(roadJunctions[i]);
                    }
                }
            }

            if (roadJunctionsEmpty)
            {
                RoadJunction newRoadJunction = FindStartPoint();
                if (newRoadJunction.location.x == -1) {
                    break;
                }
                roadJunctions.Add(newRoadJunction);
                coverageMap[(int)newRoadJunction.location.x, (int)newRoadJunction.location.y] = 2;
                int prngDirection = prng.Next(0, 4);
                GenerateRoadBranch(newRoadJunction, prngDirection, minRoadLenght, newRoadJunction, prngDirection);
            }
        }
        for (int i = 0; i < singleRoadJunctions.Count; i++) {
            coverageMap[(int)singleRoadJunctions[i].location.x, (int)singleRoadJunctions[i].location.y] = 0;
            roadJunctions.Remove(singleRoadJunctions[i]);
        }

        DrowWidthRoad();
        FindStartAndFinish();

        Debug.Log(Time.realtimeSinceStartup);
        return coverageMap;
    }

    void DrowWidthRoad()
    {
        float sqrWidthRoad = widthRoad * widthRoad;
        for (int i = 0; i < roadJunctions.Count; i++) {
            for (int t = 0; t < 4; t++)
            {
                for (int y = -(int)stapLenght; y <= (int)stapLenght; y++)
                {
                    for (int x = -(int)stapLenght; x <= (int)stapLenght; x++)
                    {
                        if (roadJunctions[i].neighbors[t].location.x != -1 && x + (int)roadJunctions[i].location.x >= 0 && x + (int)roadJunctions[i].location.x < coverageMap.GetLength(0) && y + (int)roadJunctions[i].location.y >= 0 && y + (int)roadJunctions[i].location.y < coverageMap.GetLength(1))
                        {
                            if (sqrWidthRoad >= MyMath.sqrDistanceFromPointToSection(new Vector2(x, y), new Vector2(roadJunctions[i].neighbors[t].location.x - roadJunctions[i].location.x, roadJunctions[i].neighbors[t].location.y - roadJunctions[i].location.y))) {
                                if (wholeMapData[(int)roadJunctions[i].location.x + x, (int)roadJunctions[i].location.y + y] > minFreeHeight && wholeMapData[(int)roadJunctions[i].location.x + x, (int)roadJunctions[i].location.y + y] < maxFreeHeight) {
                                    coverageMap[(int)roadJunctions[i].location.x + x, (int)roadJunctions[i].location.y + y] = 2;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void FindStartAndFinish()
    {
        maxDeep = int.MinValue;
        currentMaxDeep = int.MinValue;
        bestStartRoadJunction = new RoadJunction(new Vector2(-1, -1));
        bestFinishRoadJunction = new RoadJunction(new Vector2(-1, -1));
        for (int i = 0; i < roadJunctions.Count; i++) {
            bestCurrentStartRoadJunction = new RoadJunction(new Vector2(-1, -1));
            bestCurrentRoadJunction = roadJunctions[i];
            neighborsRoadJunctions.Clear();
            FindStartAndFinishRoadJunctions(roadJunctions[i], 0);

            bestCurrentStartRoadJunction = bestCurrentRoadJunction;
            bestCurrentRoadJunction = roadJunctions[i];

            currentMaxDeep = int.MinValue;
            neighborsRoadJunctions.Clear();
            FindStartAndFinishRoadJunctions(bestCurrentStartRoadJunction, 0);
            if (currentMaxDeep > maxDeep) {
                maxDeep = currentMaxDeep;
                bestStartRoadJunction = bestCurrentStartRoadJunction;
                bestFinishRoadJunction = bestCurrentRoadJunction;
            }
            for (int t = 0; t < neighborsRoadJunctions.Count; t++) {
                if (roadJunctions.IndexOf(neighborsRoadJunctions[t]) != -1) {
                    roadJunctions.Remove(neighborsRoadJunctions[t]);
                }
            }
            neighborsRoadJunctions.Clear();
        }
        startPoint = bestStartRoadJunction.location;
        finishPoint = bestFinishRoadJunction.location;
    }

    void FindStartAndFinishRoadJunctions(RoadJunction roadJunction, int deep)
    {
        for (int i = 0; i < 4; i++)
        {
            if (roadJunction.neighbors[i] != null && roadJunction.neighbors[i].location.x != -1 && neighborsRoadJunctions.IndexOf(roadJunction.neighbors[i]) == -1)
            {
                neighborsRoadJunctions.Add(roadJunction.neighbors[i]);
                if (currentMaxDeep < deep) {
                    currentMaxDeep = deep;
                    bestCurrentRoadJunction = roadJunction;
                }
                FindStartAndFinishRoadJunctions(roadJunction.neighbors[i], deep + 1);
            }
        }
    }

    public Vector2 getStartPoint() {
        return finishPoint;
    }
    public Vector2 getFinishPoint(){
        return startPoint;
    }

    void GenerateRoadBranch(RoadJunction roadJunction, int direction, int currentRoadLenght, RoadJunction rootRoadJunction, int rootDirection) {
        Vector2 offset = new Vector2(0, 1);

        if (roadJunction.neighbors[(direction + 2) % 4] != null && roadJunction.neighbors[(direction + 2) % 4].location.x != -1)
        {
            offset = MyMath.Rotate(roadJunction.neighbors[(direction + 2) % 4].location - roadJunction.location, 180).normalized;
        }
        else if (roadJunction.neighbors[(direction + 1) % 4] != null && roadJunction.neighbors[(direction + 1) % 4].location.x != -1)
        {
            offset = MyMath.Rotate(roadJunction.neighbors[(direction + 1) % 4].location - roadJunction.location, 270).normalized;
        }
        else if (roadJunction.neighbors[(direction + 3) % 4] != null && roadJunction.neighbors[(direction + 3) % 4].location.x != -1)
        {
            offset = MyMath.Rotate(roadJunction.neighbors[(direction + 3) % 4].location - roadJunction.location, 90).normalized;
        }
        else
        {
            offset = MyMath.Rotate(offset, direction * 90).normalized;
        }

        float sqrStapsLenght = stapLenght * stapLenght;
        float halfSqrStapsLenght = sqrStapsLenght / 2;
        float halfAngle = angle / 2;

        Vector2 bestChoiceRoadJunction = new Vector2(-1, -1);
        float midFreeHeight = (minFreeHeight + maxFreeHeight) / 2;
        float bestDifferentHeight = float.MaxValue;
        for (int localY = -(int)stapLenght; localY <= (int)stapLenght; localY++)
        {
            for (int localX = -(int)stapLenght; localX <= (int)stapLenght; localX++)
            {
                if (sqrStapsLenght > localX * localX + localY * localY)
                {
                    if (halfSqrStapsLenght < localX * localX + localY * localY)
                    {
                        Vector2 currentPoint = new Vector2(localX, localY).normalized;
                        if ((Mathf.Acos(offset.x * currentPoint.x + offset.y * currentPoint.y) / Mathf.PI) * 180 < halfAngle)
                        {
                            neighborsRoadJunctions.Clear();
                            neighborsRoadJunctions.Add(roadJunction);
                            FindNeighborsRoadJunctions(roadJunction, minRoadLenght);
                            if (CheckNextRoadJunction((int)roadJunction.location.x + localX, (int)roadJunction.location.y + localY))
                            {
                                if (Mathf.Abs(midFreeHeight - wholeMapData[(int)roadJunction.location.x + (int)localX, (int)roadJunction.location.y + (int)localY]) < bestDifferentHeight)
                                {
                                    bestDifferentHeight = Mathf.Abs(midFreeHeight - wholeMapData[(int)roadJunction.location.x + (int)localX, (int)roadJunction.location.y + (int)localY]);
                                    bestChoiceRoadJunction.x = (int)roadJunction.location.x + localX;
                                    bestChoiceRoadJunction.y = (int)roadJunction.location.y + localY;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (bestChoiceRoadJunction.x != -1)
        {
            RoadJunction nextRoadJunction = new RoadJunction(bestChoiceRoadJunction);

            nextRoadJunction.neighbors[2] = roadJunction;
            roadJunction.neighbors[direction] = nextRoadJunction;

            roadJunctions.Add(nextRoadJunction);
            coverageMap[(int)nextRoadJunction.location.x, (int)nextRoadJunction.location.y] = 2;
            GenerateRoadBranch(nextRoadJunction, 0, currentRoadLenght - 1, rootRoadJunction, rootDirection);
        }
        else {
            if (currentRoadLenght > 0 && currentRoadLenght != minRoadLenght)
            {
                if (rootRoadJunction.neighbors[rootDirection] != null) {
                    DeleteBranch(rootRoadJunction.neighbors[rootDirection]);
                }
                rootRoadJunction.neighbors[rootDirection] = new RoadJunction(new Vector2(-1, -1));
            }
            else
            {
                roadJunction.neighbors[direction] = new RoadJunction(new Vector2(-1, -1));


            }
        }
    }

    void DeleteBranch(RoadJunction roadJunction) {
        if (roadJunction.neighbors[0] != null && roadJunction.neighbors[0].location.x != -1) {
            DeleteBranch(roadJunction.neighbors[0]);
        }
        coverageMap[(int)roadJunction.location.x, (int)roadJunction.location.y] = 0;
        roadJunctions.Remove(roadJunction);
    }

    void FindNeighborsRoadJunctions(RoadJunction roadJunction, int minRoadLenght) {
        if (minRoadLenght > 0) {
            for (int i = 0; i < 4; i++) {
                if (roadJunction.neighbors[i] != null && roadJunction.neighbors[i].location.x != -1 && neighborsRoadJunctions.IndexOf(roadJunction.neighbors[i]) == -1) {
                    neighborsRoadJunctions.Add(roadJunction.neighbors[i]);
                    FindNeighborsRoadJunctions(roadJunction.neighbors[i], minRoadLenght - 1);
                }
            }
        }
    }

    RoadJunction FindStartPoint() {
        for (int y = alreadyFindStartPoint / wholeMapData.GetLength(0); y < wholeMapData.GetLength(1); y++) {
            for (int x = alreadyFindStartPoint % wholeMapData.GetLength(0); x < wholeMapData.GetLength(0); x++)
            {
                alreadyFindStartPoint++;
                neighborsRoadJunctions.Clear();
                if (CheckNextRoadJunction(x, y)) {
                    return new RoadJunction(new Vector2(x, y));
                }
            }
        }
        return new RoadJunction(new Vector2(-1, -1));
    }

    bool CheckNextRoadJunction(int x, int y) {
        if (x >= 0 && x < coverageMap.GetLength(0) && y >= 0 && y < coverageMap.GetLength(1) && wholeMapData[x, y] > (minFreeHeight - closedArea) && wholeMapData[x, y] < (maxFreeHeight - closedArea))
        {
            float sqrMinDistanceBetweenRoads = minDistanceBetweenRoads * minDistanceBetweenRoads;

            for (int localY = -(int)minDistanceBetweenRoads + y; localY <= (int)minDistanceBetweenRoads + y; localY++)
            {
                for (int localX = -(int)minDistanceBetweenRoads + x; localX <= (int)minDistanceBetweenRoads + x; localX++)
                {
                    if (localX >= 0 && localX < coverageMap.GetLength(0) && localY >= 0 && localY < coverageMap.GetLength(1))
                    {
                        if (coverageMap[localX, localY] == 2 && sqrMinDistanceBetweenRoads > (localX - x) * (localX - x) + (localY - y) * (localY - y) && CheckNeighborsRoadJunctions(localX, localY))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        else {
            return false;
        }
    }

    bool CheckNeighborsRoadJunctions(int x, int y) {
        for (int i = 0; i < neighborsRoadJunctions.Count; i++) {
            if (neighborsRoadJunctions[i].location.x == x && neighborsRoadJunctions[i].location.y == y) {
                return false;
            }
        }
        return true;
    }

    class RoadJunction {

        public Vector2 location;

        //0 up, 1 left, 2 down, 3 right
        public RoadJunction[] neighbors = new RoadJunction[4];

        public RoadJunction(Vector2 location) {
            this.location = location;
        }
    }
}
