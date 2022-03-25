using System;
using UnityEngine;

public class Goal
{
    public WorldState goalState { get; private set; }

    public Goal(WorldState goalState)
    {
        this.goalState = goalState;
    }
}
