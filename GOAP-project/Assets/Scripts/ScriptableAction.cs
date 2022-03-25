using UnityEngine;

[CreateAssetMenu]
public class ScriptableAction : ScriptableObject
{
    public int cost;
    [Space(10)]
    public WorldState effectKey;
    [Space(10)]
    public WorldState preconditionKey;
    [Space(10)]
    public AnimationClip Clip;

}
