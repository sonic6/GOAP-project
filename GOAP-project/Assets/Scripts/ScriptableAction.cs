using UnityEngine;

[CreateAssetMenu]
public class ScriptableAction : ScriptableObject
{
    public int cost;
    [Space(10)]
    public WorldState.state effectKey;
    [Space(10)]
    public WorldState.state preconditionKey;
    [Space(10)]
    public AnimationClip Clip;

}
