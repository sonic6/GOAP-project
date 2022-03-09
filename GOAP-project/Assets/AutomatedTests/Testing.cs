using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Testing : MonoBehaviour
{
    [SerializeField] string testGoal;

    public List<ScriptableAction> debugPlan;
    

    void Awake()
    {
        Goal goal = new Goal(testGoal);
        Planner planner = FindObjectOfType<Planner>();
        Plan plan = planner.DebugPlanMaker(goal);
        plan.actions.Reverse();
        debugPlan.AddRange(plan.actions);
    }
}
