using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public static class AnimationHandler
{
    static AnimationClipOverrides clipOverrides;
    static Animator animator;
    static AnimatorOverrideController overrider;

    public static void OverrideAnimation(GameObject agent, AnimationClip clip, FsmState state)
    {
        overrider = agent.GetComponent<GoapAgent>().overrider;
        animator = agent.GetComponent<Animator>();
        clipOverrides = new AnimationClipOverrides(overrider.overridesCount);
        overrider.GetOverrides(clipOverrides);
        animator.runtimeAnimatorController = overrider;
        animator.SetTrigger(state.ToString());
        clipOverrides[state.ToString()] = clip;
        overrider.ApplyOverrides(clipOverrides);
    }

    public static void ResetAnimatorTriggers(GameObject agent)
    {
        animator = agent.GetComponent<Animator>();
        animator.ResetTrigger("UseObject");
        animator.ResetTrigger("GoTo");
        animator.ResetTrigger("Animate");
    }
}

public enum FsmState
{
    UseObject,
    GoTo,
    Animate
}
