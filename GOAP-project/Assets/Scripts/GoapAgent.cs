using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class GoapAgent : MonoBehaviour
{
    public WorkingMemory memory { get; private set; }
    [SerializeField] ActionSet agentActionSet;
    [HideInInspector] public Planner planner;
    AnimatorStateMachine myMachine;

    [SerializeField] Animator animator;
    [SerializeField] AnimatorOverrideController overrider;

    List<AnimationClip> clips;

    private void Awake()
    {
        memory = new WorkingMemory();
        planner = new Planner(this);
    }

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
