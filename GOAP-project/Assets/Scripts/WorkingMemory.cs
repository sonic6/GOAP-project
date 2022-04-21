using System;
using System.Collections.Generic;
using UnityEngine;

public class WorkingMemory
{
    private List<WorldState> states = new List<WorldState>();
    private GoapAgent myAgent;

    public WorkingMemory(GoapAgent agent)
    {
        states.Add(WorldState.noState);
        myAgent = agent;
    }

    /// <summary>
    /// Adds a new memory to the working memory and then creates a new plan and executes it
    /// </summary>
    /// <param name="memory"></param>
    /// <param name="goal"></param>
    /// <param name="target"></param>
    public void AddMemory(WorldState memory, Goal goal, GameObject target = null)
    {
        states.Add(memory);

        Plan newPlan = myAgent.ObtainNewPlan(goal);
        myAgent.ExecutePlan(newPlan, target);
        //try {

        //}
        //catch { }

    }

    public List<WorldState> GetMemories()
    {
        return states;
    }

    public void RemoveMemory(WorldState memory)
    {
        states.Remove(memory);
    }
}
