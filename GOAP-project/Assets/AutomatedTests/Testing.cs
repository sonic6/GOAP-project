using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Testing : MonoBehaviour
{
    [SerializeField] WorldState goalState;
    [SerializeField] List<WorldState> testWorkingMemory = new List<WorldState>();
    public GoapAgent agent;

    public List<ScriptableAction> debugPlan;
    

    void Start()
    {
        RunTest();
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
}
