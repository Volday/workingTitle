using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectAssemble
{
    System.Random prng;
    int seed;

    StaticObjectGenerator staticObjectGenerator;

    List<HouseStaticObject> houseStaticObjects = new List<HouseStaticObject>();

    public StaticObjectAssemble(int seed, StaticObjectGenerator staticObjectGenerator) {
        this.seed = seed;
        prng = new System.Random(seed);
        this.staticObjectGenerator = staticObjectGenerator;
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
