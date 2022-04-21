using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisionSensor : MonoBehaviour
{
    NavMeshAgent navAgent;
    GoapAgent agent;

    private void Awake()
    {
        agent = GetComponentInParent<GoapAgent>();
        navAgent = agent.gameObject.GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.ToLower() == "hunted")
        {
            navAgent.SetDestination(other.transform.position);
            agent.memory.AddMemory(WorldState.playerSeen, new Goal(WorldState.playerNear));
        }
    }
}
