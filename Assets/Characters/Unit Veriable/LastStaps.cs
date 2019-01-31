using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStaps : MonoBehaviour
{
    public float timeBetweenSteps = 0.1f;
    public float trackedTime = 20;
    public int partForVector = 1;
    float maxStaps;
    public List<Vector3> steps = new List<Vector3>();
    TimeManager timeManager;

    void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeManager = gameManager.GetComponent<TimeManager>();

        maxStaps = trackedTime / timeBetweenSteps;
        for (int t = 0; t < maxStaps; t++)
        {
            steps.Add(Vector3.zero);
        }
        NextStep();
    }

    public Vector3 GetMotionVector(float time)
    {
        if (partForVector < 1)
        {
            partForVector = 1;
        }
        float distance = 0;
        float angle = 0;
        int partCounter = 0;
        int partLenght = (int)((time + 0.001f) / timeBetweenSteps);
        for (int t = steps.Count - 1; t >= (steps.Count - 1) - (partLenght * partForVector); t--)
        {
            if (partCounter < partLenght)
            {
                partCounter++;
                float nextAngle = 180;
                if (!(steps[t - 2].x == steps[t-1].x && steps[t - 2].z == steps[t-1].z) && !(steps[t].x == steps[t - 1].x && steps[t].z == steps[t - 1].z))
                {
                    nextAngle = MyMath.Angle360BetweenClockwiseVector2(new Vector2(steps[t - 2].x, steps[t - 2].z),
                    new Vector2(steps[t].x, steps[t].z), new Vector2(steps[t-1].x, steps[t-1].z));
                    Debug.Log(nextAngle);
                }
                if (nextAngle <= 180)
                {
                    angle += 180 - nextAngle;
                }
                else
                {
                    angle += -(nextAngle - 180);
                }
            }
            else
            {
                Vector3 newVector = new Vector3(steps[t + partLenght].x - steps[t].x,
                       steps[t + partLenght].y - steps[t].y, steps[t + partLenght].z - steps[t].z);
                float partDistance = Mathf.Sqrt((newVector.x * newVector.x) + (newVector.y * newVector.y) + (newVector.z * newVector.z));
                distance += partDistance;
                partCounter = 0;
                t++;
            }
        }
        Vector3 newVector2 = new Vector3(steps[steps.Count - 1].x - steps[steps.Count - 1 - partLenght].x,
                    steps[steps.Count - 1].y - steps[steps.Count - 1 - partLenght].y, steps[steps.Count - 1].z - steps[steps.Count - 1 - partLenght].z);
        angle = angle / partForVector;
        float currentAngle = MyMath.Angle360BetweenClockwiseVector2(new Vector2(newVector2.x, newVector2.z),
                    new Vector2(0, 1), new Vector2(0, 0));
        Vector2 direction = MyMath.Rotate(new Vector2(0, 1), currentAngle + angle);
        Vector2 futurePosition = direction * (distance / partForVector);
        return new Vector3(transform.position.x + futurePosition.x, transform.position.y, transform.position.z + futurePosition.y);
    }

    void NextStep()
    {
        steps.Remove(steps[0]);
        steps.Add(transform.position);
        timeManager.AddAction(NextStep, timeBetweenSteps);
    }
}
