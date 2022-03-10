using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldState
{
    public static List<states> workingMemory = new List<states>();

    public enum states
    {
        playerCaptured, 
        projectileAvailable, 
        playerSeen, 
        noState,
        meleeAvailable
    }
}
