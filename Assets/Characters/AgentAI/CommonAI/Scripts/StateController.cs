using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{

    public State currentState;
    public State remainState;

    [HideInInspector] public HealthPoints healthPoints;
    [HideInInspector] public UnitAbilities unitAbilities;
    [HideInInspector] public EnemiesAround enemiesAround;
    [HideInInspector] public TargetToAttack targetToAttack;
    [HideInInspector] public MoveSpeed moveSpeed;
    [HideInInspector] public AISkills AISkills;
    [HideInInspector] public RadiusOfView radiusOfView;
    [HideInInspector] public UnitTeam unitTeam;
    [HideInInspector] public UnitManager unitManager;
    [HideInInspector] public MapManager mapManager;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public UnitAbility nextUnitAbility;
    [HideInInspector] public PointToMove pointToMove;
    [HideInInspector] public UnitAbility abilityPending;
    [HideInInspector] public Vector3 futureTargetPosition;
    [HideInInspector] public Vector3 pointEscape;

    void Awake()
    {
        healthPoints = GetComponent<HealthPoints>();
        unitAbilities = GetComponent<UnitAbilities>();
        enemiesAround = GetComponent<EnemiesAround>();
        targetToAttack = GetComponent<TargetToAttack>();
        pointToMove = GetComponent<PointToMove>();
        moveSpeed = GetComponent<MoveSpeed>();
        AISkills = GetComponent<AISkills>();
        radiusOfView = GetComponent<RadiusOfView>();
        unitTeam = GetComponent<UnitTeam>();
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        unitManager = gameManager.GetComponent<UnitManager>();
        mapManager = gameManager.GetComponent<MapManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        navMeshAgent.speed = moveSpeed.moveSpeed;
        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }
}