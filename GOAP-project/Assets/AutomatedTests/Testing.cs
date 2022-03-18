using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Testing : MonoBehaviour
{
    [SerializeField] WorldState.state goalState;
    [SerializeField] List<WorldState.state> testWorkingMemory = new List<WorldState.state>();

    public List<ScriptableAction> debugPlan;
    

    void Awake()
    {
        RunTest();
    }

    public void RunTest()
    {
        WorldState.workingMemory.AddRange(testWorkingMemory);
        Goal goal = new Goal(goalState);
        Planner planner = FindObjectOfType<Planner>();
        Plan plan = planner.CreatePlan(goal);

        debugPlan.AddRange(plan.actions);
    }
}
