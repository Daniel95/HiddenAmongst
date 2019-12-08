using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPatrollingState : Mirror.NetworkBehaviour, IState
{
    public StateType StateType { get { return StateType.EnemyPatrollingState; } }

    [SerializeField] private float minPatrolMoveSpeed = 2.0f;
    [SerializeField] private float maxPatrolMoveSpeed = 3.5f;
    [SerializeField] private float minTimeBetweenIdleSteps = 1.2f;
    [SerializeField] private float maxTimeBetweenIdleSteps = 0.1f;
    [SerializeField] private int minIdleStepCount = 0;
    [SerializeField] private int maxIdleStepCount = 5;
    [SerializeField] private float minIdleStepLength = 0.2f;
    [SerializeField] private float maxIdleStepLength = 3.0f;
    [SerializeField] private bool randomSpeed = true;

    private List<Point> wayPoints;
    private NavMeshAgent navMeshAgent;
    private NavMeshObstacle navMeshObstacle;
    private Coroutine idleCoroutine;
    private Point currentWaypoint;

    public virtual void Act()
    {
        if (!navMeshAgent.hasPath && idleCoroutine == null)
        {
            idleCoroutine = StartCoroutine(IdleStepping(() => 
            {
                navMeshAgent.speed = GetSpeed();
                SetRandomWayPointDestination();
                idleCoroutine = null;
            }));
        }
    }

    public virtual void Enter()
    {
        navMeshAgent.speed = GetSpeed();
    }

    public virtual void Exit()
    {
        if(idleCoroutine != null)
        {
            StopCoroutine(idleCoroutine);
        }
        idleCoroutine = null;
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshObstacle = GetComponent<NavMeshObstacle>();

        SetNavMeshObstacleMode();
    }

    private void SetRandomWayPointDestination()
    {
        if(currentWaypoint != null)
        {
            WaypointManager.Instance.ClearWaypoint(currentWaypoint);
        }

        SetNavMeshMoveMode();

        List<Point> wayPoints = WaypointManager.Instance.GetAvailableWaypoints();
        currentWaypoint = wayPoints.GetRandom();
        WaypointManager.Instance.ClaimWaypoint(currentWaypoint, gameObject);
        navMeshAgent.SetDestination(currentWaypoint.transform.position);
    }

    private float GetSpeed()
    {
        if(randomSpeed)
        {
            return UnityEngine.Random.Range(minPatrolMoveSpeed, maxPatrolMoveSpeed);
        }
        else
        {
            return minPatrolMoveSpeed;
        }
    }

    private IEnumerator IdleStepping(Action onIdleCompletedEvent = null)
    {
        navMeshAgent.speed = minPatrolMoveSpeed;

        int randomSteps = UnityEngine.Random.Range(minIdleStepCount, maxIdleStepCount);

        for (int i = 0; i < randomSteps; i++)
        {
            SetNavMeshObstacleMode();

            yield return new WaitForSeconds(UnityEngine.Random.Range(minTimeBetweenIdleSteps, maxTimeBetweenIdleSteps));

            SetNavMeshMoveMode();

            Vector3 randomDirection = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f, UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;
            float randomLength = UnityEngine.Random.Range(minIdleStepLength, maxIdleStepLength);
            Vector3 destination = transform.position + (randomDirection * randomLength);

            navMeshAgent.SetDestination(destination);

            yield return null;

            while (navMeshAgent.hasPath)
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(UnityEngine.Random.Range(minTimeBetweenIdleSteps, maxTimeBetweenIdleSteps));

        if (onIdleCompletedEvent != null)
        {
            onIdleCompletedEvent();
        }
    }

    private void SetNavMeshMoveMode()
    {
        navMeshObstacle.enabled = false;
        navMeshAgent.enabled = true;
    }

    private void SetNavMeshObstacleMode()
    {
        navMeshAgent.enabled = false;
        navMeshObstacle.enabled = true;
    }
}
