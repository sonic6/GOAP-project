using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Plan : ScriptableObject
{
    /// <summary>
    /// a list of actions in a correct order which an agent will follow
    /// </summary>
    public List<ScriptableAction> actions;
}
