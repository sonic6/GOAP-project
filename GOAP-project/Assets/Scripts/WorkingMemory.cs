using System;
using System.Collections.Generic;
using UnityEngine;

public class WorkingMemory
{
    public List<WorldState> states = new List<WorldState>();

    public WorkingMemory()
    {
        states.Add(WorldState.noState);
    }
}
