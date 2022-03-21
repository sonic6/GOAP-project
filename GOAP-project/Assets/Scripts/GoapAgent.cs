using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class GoapAgent : MonoBehaviour

{
    [SerializeField] ActionSet agentActionSet;
    [HideInInspector] public Planner planner = new Planner();
    AnimatorStateMachine myMachine;

    [SerializeField] Animator animator;
    [SerializeField] AnimatorOverrideController overrider;

    List<AnimationClip> clips;

    public Plan ObtainNewPlan(Goal goal)
    {
        return planner.CreatePlan(goal, agentActionSet);
    }

    public void ExecutePlan(Plan plan)
    {
        PlanExecuter.main.Execute(plan);
    }


    public void Animate(AnimationClip clip)
    {
        
    }
}
