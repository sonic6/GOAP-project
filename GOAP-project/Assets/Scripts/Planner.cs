using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner
{
    private GoapAgent myAgent;
    private List<ScriptableAction> closedList = new List<ScriptableAction>();

    public Planner(GoapAgent agent)
    {
        myAgent = agent;
    }

    /// <summary>
    /// Creates a plan of actions based on a given goal
    /// </summary>
    /// <param name="goal"></param>
    /// <param name="agentActionSet"></param>
    /// <returns></returns>
    public Plan CreatePlan(Goal goal, ActionSet agentActionSet)
    {
        closedList.Clear(); //To begin with a clean list
        
        List<ScriptableAction> actionList = new List<ScriptableAction>();

        //The original actionSet (the scriptable object) is copied to a new list. To prevent the code from making changes to the original actionSet
        List<ScriptableAction> actionSet = new List<ScriptableAction>();
        actionSet.AddRange(agentActionSet.actions);

        Plan plan = new Plan(SearchActions(goal, actionList, actionSet));
        return plan;
    }

    //uses recursion to create a list of actions that can reach a goal and costs the least amount 
    List<ScriptableAction> SearchActions(Goal goal, List<ScriptableAction> openList, List<ScriptableAction> actionSet)
    {
        List<ScriptableAction> graphRow = new List<ScriptableAction>();
        closedList = new List<ScriptableAction>();

        //Create a row in a graph
        foreach (ScriptableAction action in actionSet)
        {
            Memory fact = new Memory() { state = action.preconditionKey };

            if (action.effectKey == goal.goalState && myAgent.memory.ContainsMatchingMemory(fact))
            {
                graphRow.Add(action);
            }
        }
        closedList.AddRange(graphRow);

        //Removes closedList items from actionSet
        actionSet = RemoveActions(actionSet, closedList);

        int maxCost = 10;
        ScriptableAction cheapestAction = graphRow[0];

        //Find cheapest action in row
        foreach(ScriptableAction action in graphRow)
        {
            if(action.cost < maxCost)
            {
                maxCost = action.cost;
                cheapestAction = action;
            }
        }
        
        openList.Add(cheapestAction);

        Goal newGoal = new Goal(cheapestAction.preconditionKey);

        List<ScriptableAction> finalGraph = new List<ScriptableAction>();

        //Recursively finds the next suitable action
        //this check stops the recursion if the new cheapest action's precondition matches a memory in the working memory
        //and that represents the end node of a A* action graph
        if (!myAgent.memory.ContainsMatchingMemory(new Memory() { state = newGoal.goalState})) 
            finalGraph.AddRange(SearchActions(newGoal, openList, actionSet));
        
        return openList;
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
