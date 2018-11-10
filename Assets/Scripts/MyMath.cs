using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyMath {

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
            return Mathf.Pow(Mathf.Abs(finishSection.y * point.x - finishSection.x * point.y) / Mathf.Sqrt(finishSection.x * finishSection.x + finishSection.y * finishSection.y), 2);
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

}
