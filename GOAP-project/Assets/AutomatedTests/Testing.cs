using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Testing : MonoBehaviour
{
    [SerializeField] WorldState.state goalState;
    [SerializeField] List<WorldState.state> testWorkingMemory = new List<WorldState.state>();

    public List<ScriptableAction> debugPlan;
    

    void Start()
    {
        RunTest();
    }

    public void RunTest()
    {
        WorldState.workingMemory.AddRange(testWorkingMemory);
        Goal goal = new Goal(goalState);
        Planner planner = FindObjectOfType<GoapAgent>().planner;
        Plan plan = FindObjectOfType<GoapAgent>().ObtainNewPlan(goal);
        FindObjectOfType<GoapAgent>().ExecutePlan(plan);

        debugPlan.AddRange(plan.actions);
    }
}
