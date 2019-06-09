using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travalotor : MonoBehaviour
{
    public List<Vector3> purposes = new List<Vector3>();
    private int purpose = 0;

    public bool circle = false;
    public float radius = 5;
    public float circleSpeed = 60;
    public Vector3 circlePoint;
    private Vector2 circleVector;

    private void Start()
    {
        circleVector = new Vector2(0, radius);
        GetComponent<PurposeOfTravel>().purposeOfTravel = purposes[purpose];
    }

    private void Update()
    {
        if (circle)
        {
            circleVector = MyMath.Rotate(circleVector, circleSpeed * Time.deltaTime);
            GetComponent<PurposeOfTravel>().purposeOfTravel = new Vector3(circleVector.x, 0, circleVector.y) + circlePoint;
            GetComponent<PointToMove>().pointToMove = new Vector3(circleVector.x, 0, circleVector.y) + circlePoint;
        }
        else
        {
            int nextPurpose;
            if (purpose + 1 == purposes.Count)
            {
                nextPurpose = 0;
            }
            else
            {
                nextPurpose = purpose + 1;
            }
            if (MyMath.sqrDistanceFromPointToPoint(transform.position, GetComponent<PurposeOfTravel>().purposeOfTravel) < 4)
            {
                GetComponent<PurposeOfTravel>().purposeOfTravel = purposes[purpose];
                purpose = nextPurpose;
            }
        }
    }
}
