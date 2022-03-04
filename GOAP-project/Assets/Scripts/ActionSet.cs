using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActionSet : ScriptableObject
{
    /// <summary>
    /// the list of all available actions for an AI agent to choose from. Different instances of this class should be used for different types of agents"
    /// </summary>
    public List<ScriptableAction> actions;
}
