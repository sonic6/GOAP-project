using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableAction : ScriptableObject
{
    public string _name;
    public int cost;
    [Space(10)]
    public WorldState.state effectKey;
    public bool effectValue;
    [Space(10)]
    public WorldState.state preconditionKey;
    public bool preconditionValue;
    [Space(10)]
    public AnimationClip Clip;

}
