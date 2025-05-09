using UnityEngine;
using UnityEngine.AI;

public class ChickenMove : MonoBehaviour
{

    enum ChickenState
    {
        Idle,
        Walking,
        Running
    }
    // GameObject destination;
    NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    ChickenState currentState = ChickenState.Idle;

    GameObject player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        // destination = GameObject.FindGameObjectWithTag("Destination");
        // if (destination != null)
        // {
        //     agent.updateRotation = false; // Disable automatic rotation
        //     // agent.SetDestination(destination.transform.position);
        // }
        // else
        // {
        //     Debug.LogError("Destination GameObject not found in the scene.");
        // }
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Check if the chicken has reached the destination
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            IdleTimer(); // Call the idle timer function
        }
        else
        {
            DetermineState(); // Call the function to determine the chicken's state
        }
    }

    void IdleTimer()
    {
        // Logic to determine if the chicken should idle
        // For example, you can set a timer to switch to idle state after a certain duration
        float idleDuration = 2f; // Duration in seconds
        Invoke("DetermineNextDestination", idleDuration);
    }

    void DetermineNextDestination()
    {
        // Logic to determine the next destination for the chicken
        // For example, you can set a new random position within a certain range
        Vector3 randomPosition = NavMesh.SamplePosition(transform.position + Random.insideUnitSphere * 10f, out NavMeshHit hit, 10f, NavMesh.AllAreas) ? hit.position : transform.position;
        agent.SetDestination(randomPosition);
    }

    void DetermineState()
    {
        // Determine the chicken's state based on its speed
        if (agent.velocity.magnitude < 0.1f)
        {
            // Idle state
            Debug.Log("Chicken is idle.");
            currentState = ChickenState.Idle;

        }
        else if (Vector3.Distance(transform.position, player.transform.position) < 5f)
        {
            // Running state
            Debug.Log("Chicken is running towards the player.");
            agent.SetDestination(player.transform.position);
            currentState = ChickenState.Running;
        }
        else if (agent.velocity.magnitude < 2.0f)
        {
            // Walking state
            Debug.Log("Chicken is walking.");
            currentState = ChickenState.Walking;
        }
 
    }
}
