using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProximitySensor
{
    GoapAgent myAgent;
    NavMeshAgent nav;
    public bool searching = false;

    public ProximitySensor(GoapAgent agent)
    {
        myAgent = agent;
        nav = myAgent.GetComponent<NavMeshAgent>();
    }

    public void IfNearTarget()
    {
        if (searching && Vector3.Distance(myAgent.transform.position, nav.destination) < .5f)
        {
            searching = false;
            myAgent.destination = myAgent.destination.GetNextPoint();
            nav.SetDestination(myAgent.destination.transform.position);

            //Create new goal and plan and execute
            Goal newGoal = new Goal(WorldState.playerSeen);
            Plan newPlan = myAgent.ObtainNewPlan(newGoal);
            myAgent.ExecutePlan(newPlan, null);
        }
    }
}
