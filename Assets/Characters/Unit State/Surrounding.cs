using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surrounding : MonoBehaviour
{
    public float timeToNextSurround = 3;
    public float minDistanceToUpdate = 3;
    public Vector3 surroundPosition = Vector3.zero;

    private void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        TimeManager timeManager = gameManager.GetComponent<TimeManager>();
        timeManager.AddAction(StopSurrounding, timeToNextSurround, this);
    }

    void StopSurrounding() {
        Destroy(this);
    }

    public bool CheckForUpdatePisition(GameObject target) {
        if (MyMath.sqrDistanceFromPointToPoint(target.transform.position, transform.position) > minDistanceToUpdate * minDistanceToUpdate)
        {
            return true;
        }
        else {
            return false;
        }
    }
}
