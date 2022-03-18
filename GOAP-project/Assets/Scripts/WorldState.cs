using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldState
{
    public static List<state> workingMemory = new List<state>();

    public enum state
    {
        playerCaptured, 
        projectileAvailable, 
        playerSeen, 
        noState,
        meleeAvailable
    }
}
