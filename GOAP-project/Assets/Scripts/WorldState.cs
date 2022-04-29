using UnityEngine; 

public enum WorldState
{
    playerCaptured,
    projectileAvailable,
    targetSeen,
    noState,
    meleeAvailable, 
    targetNear,
    IsHiding, 
    FoundKey
}

public struct Memory
{
    public GameObject target;
    public WorldState state;
}
