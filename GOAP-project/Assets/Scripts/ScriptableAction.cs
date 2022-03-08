using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableAction : ScriptableObject
{
    public string _name;
    public int cost;
    [Space(10)]
    public string effectKey;
    public bool effectValue;
    [Space(10)]
    public string preconditionKey;
    public bool preconditionValue;
    [Space(10)]
    public AnimationClip Clip;

}
