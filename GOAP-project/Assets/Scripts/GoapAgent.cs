using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class GoapAgent : MonoBehaviour
{
    Planner planner;
    AnimatorStateMachine myMachine;

    [SerializeField] Animator animator;
    [SerializeField] AnimatorOverrideController overrider;

    List<AnimationClip> clips;

    private void Start()
    {
        
    }

    public void Animate(AnimationClip clip)
    {
        
    }
}
