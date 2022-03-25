using System;
using UnityEngine;

public class Goal
{
    public WorldState.state goalState { get; private set; }

    public Goal(WorldState.state goalState)
    {
        this.goalState = goalState;
    }
}
