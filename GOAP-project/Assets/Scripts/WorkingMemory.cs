using System;
using System.Collections.Generic;
using UnityEngine;

public class WorkingMemory
{
    private List<Memory> memories = new List<Memory>();
    private GoapAgent myAgent;

    public WorkingMemory(GoapAgent agent)
    {
        memories.Add(new Memory { state = WorldState.noState });
        myAgent = agent;
    }

    /// <summary>
    /// Adds a new memory to the working memory and then creates a new plan and executes it
    /// </summary>
    /// <param name="memory"></param>
    /// <param name="goal"></param>
    public void AddMemory(Memory memory, Goal goal)
    {
        //Don't add the memory if it already exists 
        if(ContainsMatchingMemory(memory) == false)
            memories.Add(memory);

        Plan newPlan = myAgent.ObtainNewPlan(goal);
        myAgent.ExecutePlan(newPlan, memory.target);

    }

    public List<Memory> GetMemories()
    {
        return memories;
    }

    //Remove the memory that matches with the provided memory
    public void RemoveMemory(Memory memory, Goal goal)
    {
        foreach(Memory fact in memories)
        {
            if (fact.state == memory.state && fact.target == memory.target)
            {
                memories.Remove(fact);
                Plan newPlan = myAgent.ObtainNewPlan(goal);
                myAgent.ExecutePlan(newPlan, memory.target);

                Debug.Log("removed memory ");
                break;
            }
        }
    }

    //Checks if there a given worldfact matches a worldfact in memory
    public bool ContainsMatchingMemory(Memory worldFact)
    {
        foreach(Memory fact in memories)
        {
            if(fact.state == worldFact.state)
            {
                return true;
            }
        }
        return false;
    }
}
