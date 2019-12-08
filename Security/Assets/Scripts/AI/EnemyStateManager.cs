using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStateManager : Mirror.NetworkBehaviour
{
    public static EnemyStateManager Instance { get { return GetInstance(); } }
    private static EnemyStateManager instance;
    private static EnemyStateManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<EnemyStateManager>();
        }
        return instance;
    }

    private StateMachine stateMachine;
    private NavMeshAgent navMeshAgent;
    private NavMeshPath navMeshPath;
    private Animator animator;

    public void StartCivilian()
    {
        animator = GetComponentInChildren<Animator>();
        stateMachine = GetComponent<StateMachine>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();
        stateMachine.SwitchState(StateType.EnemyPatrollingState);
    }
}
