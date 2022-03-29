using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class Testing : MonoBehaviour
{
    [SerializeField] WorldState goalState;
    [SerializeField] List<WorldState> testWorkingMemory = new List<WorldState>();
    public GoapAgent agent;

    public List<ScriptableAction> debugPlan;


    #region "animations"
    public Animator animator;
    public AnimatorOverrideController overrider;
    public AnimationClip clip;
    protected AnimationClipOverrides clipOverrides;
    #endregion



    void Awake()
    {
        //RunTest();
    }

    public void RunTest()
    {
        agent.memory.states.AddRange(testWorkingMemory);
        Goal goal = new Goal(goalState);
        Planner planner = FindObjectOfType<GoapAgent>().planner;
        Plan plan = FindObjectOfType<GoapAgent>().ObtainNewPlan(goal);
        FindObjectOfType<GoapAgent>().ExecutePlan(plan);

        debugPlan.AddRange(plan.GetActions());
    }

    public void OverrideAnimation(AnimationClip clip)
    {
        clipOverrides = new AnimationClipOverrides(overrider.overridesCount);
        overrider.GetOverrides(clipOverrides);
        animator.runtimeAnimatorController = overrider;
        animator.SetTrigger("UseObject");
        //overrider.animationClips.SetValue(clip, 0);
        clipOverrides["UseObject"] = clip;
        //animator.SetBool("SmoothSwitch", true);
        overrider.ApplyOverrides(clipOverrides);
    }
}
