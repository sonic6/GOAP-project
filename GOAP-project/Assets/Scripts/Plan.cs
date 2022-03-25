using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plan
{
    /// <summary>
    /// a list of actions in a correct order which an agent will follow
    /// </summary>
    private List<ScriptableAction> actions = new List<ScriptableAction>();

    public Plan(List<ScriptableAction> actions)
    {
        this.actions = actions;
        this.actions.Reverse();
    }

    public List<ScriptableAction> GetActions()
    {
        return actions;
    }
}
