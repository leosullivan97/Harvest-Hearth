using UnityEngine;
using UnityEngine.AI;

public class ChickenMove : MonoBehaviour
{
    GameObject destination;
    NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    enum ChickenState
    {
        Idle,
        Walking,
        Running
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = GameObject.FindGameObjectWithTag("Destination");
        if (destination != null)
        {
            agent.updateRotation = false; // Disable automatic rotation
            agent.SetDestination(destination.transform.position);
        }
        else
        {
            Debug.LogError("Destination GameObject not found in the scene.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the chicken has reached the destination
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            transform.Rotate(0, 0, 0);
        }
        else        {
            // Move the chicken towards the destination
            agent.SetDestination(destination.transform.position);
            transform.Rotate(0, 0, 0);
        }
    }

    void DetermineState()
    {
        // Determine the chicken's state based on its speed
        if (agent.velocity.magnitude < 0.1f)
        {
            // Idle state
            Debug.Log("Chicken is idle.");
        }
        else if (agent.velocity.magnitude < 2.0f)
        {
            // Walking state
            Debug.Log("Chicken is walking.");
        }
        else
        {
            // Running state
            Debug.Log("Chicken is running.");
        }
    }
}
