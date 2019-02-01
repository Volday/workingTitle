using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStaps : MonoBehaviour
{
    public float timeBetweenSteps = 0.1f;
    public float trackedTime = 20;
    public float beginTail = 1;
    public float finishTail = 2;
    float maxStaps;
    public List<Vector3> steps = new List<Vector3>();
    TimeManager timeManager;

    MoveSpeed moveSpeed;

    void Start()
    {
        moveSpeed = GetComponent<MoveSpeed>();

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
        float distance = 0;
        float angle = 0;

        int partLenght = (int)((time + 0.001f) / timeBetweenSteps);

        int t = steps.Count - 1 - partLenght;
        float nextAngle = 180;
        if (!(steps[t - partLenght].x == steps[t].x && steps[t - partLenght].z == steps[t].z) && !(steps[t + partLenght].x == steps[t].x && steps[t + partLenght].z == steps[t].z))
        {
            nextAngle = MyMath.Angle360BetweenClockwiseVector2(new Vector2(steps[t - partLenght].x, steps[t - partLenght].z),
            new Vector2(steps[t + partLenght].x, steps[t + partLenght].z), new Vector2(steps[t].x, steps[t].z));
        }
        if (nextAngle <= 180)
        {
            angle += 180 - nextAngle;
        }
        else
        {
            angle += -(nextAngle - 180);
        }

        Vector3 lastPartDistance = new Vector3(steps[t + partLenght].x - steps[t].x,
                steps[t + partLenght].y - steps[t].y, steps[t + partLenght].z - steps[t].z);
        float partDistance = Mathf.Sqrt((lastPartDistance.x * lastPartDistance.x) + (lastPartDistance.y * lastPartDistance.y) + (lastPartDistance.z * lastPartDistance.z));
        distance += partDistance;

        Vector3 currentDirection = new Vector3(steps[steps.Count - 1].x - steps[steps.Count - 1 - partLenght].x,
                    steps[steps.Count - 1].y - steps[steps.Count - 1 - partLenght].y, steps[steps.Count - 1].z - steps[steps.Count - 1 - partLenght].z);
        float currentAngle = MyMath.Angle360BetweenClockwiseVector2(new Vector2(currentDirection.x, currentDirection.z),
                    new Vector2(0, 1), new Vector2(0, 0));

        if (partDistance != 0) {
            Vector3 fullTailVector = new Vector3(steps[t + partLenght].x - steps[t + partLenght - (int)(partLenght * finishTail)].x,
                steps[t + partLenght].y - steps[t + partLenght - (int)(partLenght * finishTail)].y, steps[t + partLenght].z - steps[t + partLenght - (int)(partLenght * finishTail)].z);
            float fullTailDistance = Mathf.Sqrt((fullTailVector.x * fullTailVector.x) + (fullTailVector.y * fullTailVector.y) + (fullTailVector.z * fullTailVector.z));

            float tailCoefficient = fullTailDistance / (moveSpeed.moveSpeed * time);

            if (tailCoefficient > beginTail)
            {
                tailCoefficient = 1 - (finishTail - tailCoefficient) / (finishTail - beginTail);
                float nextTailAngle = 180;
                if (!(steps[t + partLenght - (int)(partLenght * finishTail)].x == steps[t].x &&
                    steps[t + partLenght - (int)(partLenght * finishTail)].z == steps[t].z) &&
                    !(steps[t + partLenght].x == steps[t].x && steps[t + partLenght].z == steps[t].z))
                {
                    nextTailAngle = MyMath.Angle360BetweenClockwiseVector2(new Vector2(steps[t + partLenght - (int)(partLenght * finishTail)].x, steps[t + partLenght - (int)(partLenght * finishTail)].z),
                    new Vector2(steps[t + partLenght].x, steps[t + partLenght].z), new Vector2(steps[t].x, steps[t].z));
                }
                float tailAngle = 0;
                if (nextTailAngle <= 180)
                {
                    tailAngle += 180 - nextTailAngle;
                }
                else
                {
                    tailAngle += -(nextTailAngle - 180);
                }
                angle -= tailAngle * tailCoefficient;
            }
        }

        Vector2 direction = MyMath.Rotate(new Vector2(0, 1), currentAngle + angle);
        Vector2 futurePosition = direction * distance;
        return new Vector3(transform.position.x + futurePosition.x, transform.position.y, transform.position.z + futurePosition.y);
    }

    void NextStep()
    {
        steps.Remove(steps[0]);
        steps.Add(transform.position);
        timeManager.AddAction(NextStep, timeBetweenSteps, gameObject);
    }
}
