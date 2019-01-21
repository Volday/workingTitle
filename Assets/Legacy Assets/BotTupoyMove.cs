using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotTupoyMove : MonoBehaviour {

    public Transform target;

    [HideInInspector] public NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
    }

    private void Update()
    {
        navMeshAgent.destination = target.position;
    }
}
