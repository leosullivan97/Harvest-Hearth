using UnityEngine;
using UnityEngine.AI;

public class ChickenMove : MonoBehaviour
{
    GameObject destination;
    NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
}
