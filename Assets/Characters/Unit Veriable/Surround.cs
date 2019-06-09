using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surround : MonoBehaviour
{
    private List<GameObject> surrounders = new List<GameObject>();

    public void AddSurround(GameObject objectToAdd) {
        surrounders.Add(objectToAdd);
    }

    public void RemoveSurround(GameObject objectToRemove) {
        surrounders.Remove(objectToRemove);
    }

    public Vector3 GetPositionToSurround(GameObject surrounder, float range) {
        if (!surrounders.Contains(surrounder)) {
            AddSurround(surrounder);
        }
        int surrounderIndex = surrounders.IndexOf(surrounder);
        float surroundRange = 360 / surrounders.Count;
        int shiftAngle = surrounder.GetComponent<AISkills>().GetRandomNumber(0, (int)surroundRange);
        float surroundAngle = surroundRange * surrounderIndex - shiftAngle;
        Vector2 pointToSurround2D = MyMath.Rotate(new Vector2(0, range + GetComponent<CapsuleCollider>().radius), surroundAngle);
        Vector3 futurePosition = transform.position;//GetComponent<LastStaps>().GetMotionVector(1);

        float minDistance = GetComponent<CapsuleCollider>().radius + surrounder.GetComponent<CapsuleCollider>().radius;
        Vector2 reversPointToSurround2D = pointToSurround2D * (-1);
        reversPointToSurround2D *= 1 - (minDistance) / reversPointToSurround2D.magnitude;
        reversPointToSurround2D *= surrounder.GetComponent<AISkills>().
            GetRandomNumber(0, (int)reversPointToSurround2D.magnitude) / reversPointToSurround2D.magnitude;
        pointToSurround2D += reversPointToSurround2D;

        Ray ray = new Ray(futurePosition, new Vector3(pointToSurround2D.x, 0, pointToSurround2D.y));
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, pointToSurround2D.magnitude))
        {
            float distanceToHit = MyMath.sqrDistanceFromPointToPoint(hit.point, futurePosition);
            float distanceCoefficient = (distanceToHit - GetComponent<CapsuleCollider>().radius) / pointToSurround2D.magnitude;
            pointToSurround2D *= distanceCoefficient;
        }
        Vector3 pointToSurround = new Vector3(futurePosition.x + pointToSurround2D.x, futurePosition.y, futurePosition.z + pointToSurround2D.y);
        return pointToSurround;
    }    

    private void Update()
    {
        for (int t = 0; t < surrounders.Count; t++) {
            if (!surrounders[t].activeSelf || surrounders[t] == null) {
                surrounders.RemoveAt(t);
            }
        }
    }
}
