using UnityEngine; 

public enum WorldState
{
    playerCaptured,
    projectileAvailable,
    playerSeen,
    noState,
    meleeAvailable, 
    playerNear
}

public struct Memory
{
    public GameObject target;
    public WorldState state;
}
