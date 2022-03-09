using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner : MonoBehaviour
{
    [SerializeField] ActionSet agentActionSet;
    
    private List<ScriptableAction> closed = new List<ScriptableAction>();
    

    private void Start()
    {

    }

    /// <summary>
    /// Creates a plan using a A* algorithm for an agent to follow
    /// </summary>
    /// <param name="goal"></param>
    /// <returns></returns>
    public Plan CreatePlan(Goal goal)
    {
        return new Plan();
    }

    public Plan DebugPlanMaker(Goal goal)
    {
        Plan plan = new Plan();
        //plan.actions = new List<ScriptableAction>();

        ////Not a real A* algorithm. Just for testing

        //while (goal._name != string.Empty)
        //{
        //    foreach (ScriptableAction action in agentActionSet.actions)
        //    {
        //        if (action.effectKey == goal._name)
        //        {
        //            List<ScriptableAction> _actions;
        //            _actions = plan.actions;
        //            _actions.Add(action);
        //            goal._name = action.preconditionKey;
        //        }
        //    }
        //}

        List<ScriptableAction> graph = new List<ScriptableAction>();
        List<ScriptableAction> actionSet = new List<ScriptableAction>();
        actionSet.AddRange(agentActionSet.actions);
        plan.actions.AddRange(CreateAStarPlan(goal, graph, actionSet));

        return plan;
    }

    //uses recursion to create a list of actions that can reach a goal and costs the least amount 
    List<ScriptableAction> CreateAStarPlan(Goal goal, List<ScriptableAction> graph, List<ScriptableAction> actionSet)
    {
        List<ScriptableAction> currentRow = new List<ScriptableAction>();

        //Create a row in graph
        foreach(ScriptableAction action in actionSet)
        {
            if (action.effectKey == goal._name)
            {
                currentRow.Add(action);
            }
        }
        closed = currentRow;
        actionSet = RemoveActions(actionSet, closed);

        int maxCost = 10;
        ScriptableAction cheapestAction = currentRow[0];

        //Find cheapest action in row
        foreach(ScriptableAction action in currentRow)
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
        //this check should check the world state instead of if goal has empty name =>
        // => implement that when a world representation system has been implemented
        if (newGoal._name != string.Empty) 
            finalGraph.AddRange(CreateAStarPlan(newGoal, graph, actionSet));

        return graph;
    }

    List<ScriptableAction> RemoveActions(List<ScriptableAction> allActions, List<ScriptableAction> removableActions)
    {
        foreach(ScriptableAction action in removableActions)
        {
            allActions.Remove(action);
        }
        return allActions;
    }
}
