using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Testing : MonoBehaviour
{
    [SerializeField] WorldState.states goalState;
    [SerializeField] List<WorldState.states> testWorkingMemory = new List<WorldState.states>();

    public List<ScriptableAction> debugPlan;
    

    void Awake()
    {
        WorldState.workingMemory.AddRange(testWorkingMemory);
        Goal goal = new Goal(goalState);
        Planner planner = FindObjectOfType<Planner>();
        Plan plan = planner.DebugPlanMaker(goal);
        plan.actions.Reverse();
        debugPlan.AddRange(plan.actions);
    }
}
