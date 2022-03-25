using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldState
{
    //This working memory list is just for debugging. Each agent should have its own working memory
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
