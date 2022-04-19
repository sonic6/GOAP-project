using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProximitySensor
{
    GoapAgent myAgent;
    NavMeshAgent nav;
    public GameObject target;
    public bool chase = false; //means that the agent is chasing the target

    public ProximitySensor(GoapAgent agent)
    {
        myAgent = agent;
        nav = myAgent.GetComponent<NavMeshAgent>();
    }

    public void IfNearTarget()
    {
        if (chase && Vector3.Distance(myAgent.transform.position, target.transform.position) < .5f)
        {
            Debug.Log("dsdsd");
            chase = false;
            myAgent.memory.AddMemory(WorldState.playerNear, new Goal(WorldState.playerCaptured), target);
        }
    }
}
