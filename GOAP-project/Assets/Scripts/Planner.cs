using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner
{
    public List<ScriptableAction> closed = new List<ScriptableAction>();

    /// <summary>
    /// Creates a plan of actions based on a given goal
    /// </summary>
    /// <param name="goal"></param>
    /// <param name="agentActionSet"></param>
    /// <returns></returns>
    public Plan CreatePlan(Goal goal, ActionSet agentActionSet)
    {
        closed.Clear(); //To begin with a clean list
        Plan plan = new Plan();

        List<ScriptableAction> graph = new List<ScriptableAction>();

        //The original actionSet (the scriptable object) is copied to a new list. To prevent the code from making changes to the original actionSet
        List<ScriptableAction> actionSet = new List<ScriptableAction>();
        actionSet.AddRange(agentActionSet.actions); 

        plan.actions.AddRange(CreateActionGraph(goal, graph, actionSet));
        plan.actions.Reverse();

        return plan;
    }

    //uses recursion to create a list of actions that can reach a goal and costs the least amount 
    List<ScriptableAction> CreateActionGraph(Goal goal, List<ScriptableAction> graph, List<ScriptableAction> actionSet)
    {
        List<ScriptableAction> opened = new List<ScriptableAction>();

        //Create a row in graph
        foreach (ScriptableAction action in actionSet)
        {
            if (action.effectKey == goal.goalState && WorldState.workingMemory.Contains(action.preconditionKey))
            {
                opened.Add(action);
            }
        }
        closed.AddRange(opened);
        actionSet = RemoveActions(actionSet, closed);

        int maxCost = 10;
        ScriptableAction cheapestAction = opened[0];

        //Find cheapest action in row
        foreach(ScriptableAction action in opened)
        {
            if(action.cost < maxCost)
            {
                maxCost = action.cost;
                cheapestAction = action;
            }
        }
        
        graph.Add(cheapestAction);

        Goal newGoal = new Goal(cheapestAction.preconditionKey);

        List<ScriptableAction> finalGraph = new List<ScriptableAction>();

        //Recursively finds the next suitable action
        //this check stops the recursion if an action has been found that does not have a precondition
        //and that represents the end node of a A* action graph
        if (newGoal.goalState != WorldState.state.noState) 
            finalGraph.AddRange(CreateActionGraph(newGoal, graph, actionSet));
        
        return graph;
    }

    //Removes one set of actions from another set of actions 
    List<ScriptableAction> RemoveActions(List<ScriptableAction> allActions, List<ScriptableAction> removableActions)
    {
        foreach(ScriptableAction action in removableActions)
        {
            allActions.Remove(action);
        }
        return allActions;
    }
}
