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
    public AnimatorOverrideController overrider;

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
        StartCoroutine(PlanExecuter.main.Execute(plan, gameObject));
    }


    public void Animate(AnimationClip clip)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.ToLower() == "hunted")
        {
            memory.states.Add(WorldState.playerSeen);
            Plan myPlan = ObtainNewPlan(new Goal(WorldState.playerCaptured));
            ExecutePlan(myPlan);
        }
    }

    
    public void AddMemory(WorldState newMemory)
    {
        memory.states.Add(newMemory);
    }
    
}
