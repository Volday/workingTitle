using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestinationCheck : MonoBehaviour
{

    private CubeInTarget target;
    private NavMeshAgent navMeshAgent;
    private Vector3 currentDestination;

    void Start()
    {
        currentDestination = new Vector3(1, 2, 3);
        target = GetComponent<CubeInTarget>();
        navMeshAgent = target.target.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (currentDestination != navMeshAgent.destination)
        {
            //Debug.Log(navMeshAgent.destination + " " + Time.time);
            currentDestination = navMeshAgent.destination;
        }
    }
}
