using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyMath {

    public static float PointToRightOfLine(Vector2 point, Vector2 finishSection, Vector2 startSection)
    {
        //D = (х3 - х1) * (у2 - у1) - (у3 - у1) * (х2 - х1)
        return (point.x - startSection.x) * (finishSection.y - startSection.y) - (point.y - startSection.y) * (finishSection.x - startSection.x);
    }

    public static float sqrDistanceFromPointToPoint(Vector3 point1, Vector3 point2) {
        Vector3 differenceVector = point1 - point2;
        float distance = (differenceVector.x * differenceVector.x) + (differenceVector.y * differenceVector.y) + (differenceVector.z * differenceVector.z);
        return distance;
    }

    public static float sqrDistanceFromPointToPoint(Vector2 point1, Vector2 point2)
    {
        Vector2 differenceVector = point1 - point2;
        float distance = (differenceVector.x * differenceVector.x) + (differenceVector.y * differenceVector.y);
        return distance;
    }

    public static float Angle360BetweenClockwiseVector2(Vector2 point, Vector2 finishSection, Vector2 startSection)
    {
        float D = (point.x - startSection.x) * (finishSection.y - startSection.y) - (point.y - startSection.y) * (finishSection.x - startSection.x);
        finishSection.x -= startSection.x;
        finishSection.y -= startSection.y;
        point.x -= startSection.x;
        point.y -= startSection.y;
        if (D <= 0)
        {
            return Vector2.Angle(finishSection, point);
        }
        else {
            return 360 - Vector2.Angle(finishSection, point);
        }
    }

    public static float sqrDistanceFromPointToSection(Vector2 point, Vector2 finishSection, Vector2 startSection) {
        finishSection.x -= startSection.x;
        finishSection.y -= startSection.y;
        point.x -= startSection.x;
        point.y -= startSection.y;
        return sqrDistanceFromPointToSection(point, finishSection);
    }

    public static float sqrDistanceFromPointToSection(Vector2 point, Vector2 finishSection)
    {
        float sqrSideA = (point.x - finishSection.x) * (point.x - finishSection.x) + (point.y - finishSection.y) * (point.y - finishSection.y);
        float sqrSideB = point.x * point.x + point.y * point.y;
        float sqrSideC = finishSection.x * finishSection.x + finishSection.y * finishSection.y;
        if (sqrSideA + sqrSideC - sqrSideB < 0)
        {
            return sqrSideA;
        }
        else if (sqrSideB + sqrSideC - sqrSideA < 0)
        {
            return sqrSideB;
        }
        else {
            return Mathf.Pow(Mathf.Abs(finishSection.y * point.x - finishSection.x * point.y) / Mathf.Sqrt(sqrSideC), 2);
        }
    }

    public static float sqrDistanceFromPointToRay(Vector2 point, Vector2 finishSection, Vector2 startSection)
    {
        finishSection.x -= startSection.x;
        finishSection.y -= startSection.y;
        point.x -= startSection.x;
        point.y -= startSection.y;
        return sqrDistanceFromPointToRay(point, finishSection);
    }

    public static float sqrDistanceFromPointToRay(Vector2 point, Vector2 finishSection)
    {
        float sqrSideA = (point.x - finishSection.x) * (point.x - finishSection.x) + (point.y - finishSection.y) * (point.y - finishSection.y);
        float sqrSideB = point.x * point.x + point.y * point.y;
        float sqrSideC = finishSection.x * finishSection.x + finishSection.y * finishSection.y;
        if (sqrSideB + sqrSideC - sqrSideA < 0)
        {
            return sqrSideB;
        }
        else
        {
            return Mathf.Pow(Mathf.Abs(finishSection.y * point.x - finishSection.x * point.y) / Mathf.Sqrt(sqrSideC), 2);
        }
    }

    public static Vector2 Rotate(Vector2 point, float angle)
    {
        angle = (angle / 180.0f) * Mathf.PI;
        Vector2 rotated_point;
        rotated_point.x = point.x * Mathf.Cos(angle) - point.y * Mathf.Sin(angle);
        rotated_point.y = point.x * Mathf.Sin(angle) + point.y * Mathf.Cos(angle);
        return rotated_point;
    }

    public static Vector2[] MixVector2(Vector2[] vector, int seed) {
        System.Random prng = new System.Random(seed);

        for (int i = 0; i < vector.Length; i++){
            int temp = prng.Next(0, vector.Length);
            Vector2 temporaryVector = vector[temp];
            vector[temp] = vector[i];
            vector[i] = temporaryVector;
        }

        return vector;
    }

    //public static bool PointBelongsQuadrilateral(Vector2 point, Vector2 quadrilateralPoint1, Vector2 quadrilateralPoint2, Vector2 quadrilateralPoint3, Vector2 quadrilateralPoint4) {
    //    if (PointBelongsTriangle(point, quadrilateralPoint1, quadrilateralPoint2, quadrilateralPoint3) ||
    //        PointBelongsTriangle(point, quadrilateralPoint3, quadrilateralPoint4, quadrilateralPoint1) ||
    //        PointBelongsTriangle(point, quadrilateralPoint3, quadrilateralPoint4, quadrilateralPoint2) ||
    //        PointBelongsTriangle(point, quadrilateralPoint4, quadrilateralPoint2, quadrilateralPoint1)
    //        )
    //    {
    //        return true;
    //    }
    //    else {
    //        return false;
    //    }
    //}

    public static bool PointBelongsTriangle(Vector2 point, Vector2 trianglePoint1, Vector2 trianglePoint2, Vector2 trianglePoint3)
    {
        float a = (trianglePoint1.x - point.x) * (trianglePoint2.y - trianglePoint1.y) - (trianglePoint2.x - trianglePoint1.x) * (trianglePoint1.y - point.y);
        float b = (trianglePoint2.x - point.x) * (trianglePoint3.y - trianglePoint2.y) - (trianglePoint3.x - trianglePoint2.x) * (trianglePoint2.y - point.y);
        float c = (trianglePoint3.x - point.x) * (trianglePoint1.y - trianglePoint3.y) - (trianglePoint1.x - trianglePoint3.x) * (trianglePoint3.y - point.y);

        if ((a >= 0 && b >= 0 && c >= 0) || (a <= 0 && b <= 0 && c <= 0))
        {
            return true;
        }
        else {
            return false;
        }
    }

    public static bool PointBelongsCircle(Vector2 point, Vector2 centor, float radius)
    {
        point = new Vector2(point.x - centor.x, point.y - centor.y);

        if ((point.x * point.x + point.y * point.y) <= radius * radius)
        {
            return true;
        }
        else {
            return false;
        }
    }
}
