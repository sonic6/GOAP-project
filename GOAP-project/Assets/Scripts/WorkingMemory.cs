using System;
using System.Collections.Generic;
using UnityEngine;

public class WorkingMemory
{
    private List<WorldFact> states = new List<WorldFact>();
    private GoapAgent myAgent;

    public WorkingMemory(GoapAgent agent)
    {
        states.Add(new WorldFact { state = WorldState.noState });
        myAgent = agent;
    }

    /// <summary>
    /// Adds a new memory to the working memory and then creates a new plan and executes it
    /// </summary>
    /// <param name="memory"></param>
    /// <param name="goal"></param>
    public void AddMemory(WorldFact memory, Goal goal)
    {
        states.Add(memory);

        Plan newPlan = myAgent.ObtainNewPlan(goal);
        myAgent.ExecutePlan(newPlan, memory.target);

    }

    public List<WorldFact> GetMemories()
    {
        return states;
    }

    //Remove the memory that matches with the provided memory
    public void RemoveMemory(WorldFact memory, Goal goal)
    {
        foreach(WorldFact fact in states)
        {
            if (fact.state == memory.state && fact.target == memory.target)
            {
                states.Remove(fact);
                Plan newPlan = myAgent.ObtainNewPlan(goal);
                myAgent.ExecutePlan(newPlan, memory.target);

                Debug.Log("removed memory ");
                break;
            }
        }
    }

    //Checks if there a given worldfact matches a worldfact in memory
    public bool ContainsMatchingMemory(WorldFact worldFact)
    {
        foreach(WorldFact fact in states)
        {
            if(fact.state == worldFact.state/* && fact.target == worldFact.target*/)
            {
                return true;
            }
        }
        return false;
    }
}
