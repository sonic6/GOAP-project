using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableAction : ScriptableObject
{
    public string _name;
    [Space(10)]
    public string _effectKey;
    public bool _effectValue;
    [Space(10)]
    public string _preconditionKey;
    public bool _preconditionValue;

}
