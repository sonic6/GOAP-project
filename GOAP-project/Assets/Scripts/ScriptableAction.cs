using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableAction : ScriptableObject
{
    [SerializeField] protected string _name;
    [SerializeField] protected int cost;
    [Space(10)]
    [SerializeField] protected string effectKey;
    [SerializeField] protected bool effectValue;
    [Space(10)]
    [SerializeField] protected string preconditionKey;
    [SerializeField] protected bool preconditionValue;
    [Space(10)]
    [SerializeField] protected AnimationClip Clip;

}
