using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner : MonoBehaviour
{
    [SerializeField] ActionSet agentActionSet;

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
        plan.actions = new List<ScriptableAction>();
        //string precondition = "prec";

        //Not a real A* algorithm. Just for testing
        
        while(goal._name != string.Empty)
        {
            foreach (ScriptableAction action in agentActionSet.actions)
            {
                if (action.effectKey == goal._name)
                {
                    List<ScriptableAction> _actions;
                    _actions = plan.actions;
                    _actions.Add(action);
                    goal._name = action.preconditionKey;
                }
            }
        }

        return plan;
    }
}
