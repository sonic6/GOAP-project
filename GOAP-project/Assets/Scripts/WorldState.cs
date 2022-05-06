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
    FoundAllKeys, 
    GrabbedKey
}

public struct Memory
{
    public GameObject target;
    public WorldState state;
}
