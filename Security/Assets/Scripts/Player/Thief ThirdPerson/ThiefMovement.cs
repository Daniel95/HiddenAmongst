using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class ThiefMovement : MonoBehaviour
{
    private NavMeshAgent naveMeshAgent;
    private Rigidbody rigidbody;

    public void Move(Vector3 direction, bool crouch, bool jump)
    {
        naveMeshAgent.velocity = direction * naveMeshAgent.speed;
    }

    private void Start()
    {
        naveMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }
}