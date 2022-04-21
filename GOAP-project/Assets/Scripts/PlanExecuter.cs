using System.Collections;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlanExecuter : MonoBehaviour
{
    public static PlanExecuter main;
    private GameObject currentAgent; //The agent whose plan will be executed
    private GameObject currentTarget; //The gameobject that is targeted by an action
    private WaitCondition currentWaitCondition;
    private bool NextAction = false;

    private void Awake()
    {
        main = this;
    }

    private void Update()
    {
        NextAction = ConditionChecker(currentWaitCondition);
    }

    public IEnumerator Execute(Plan plan, GameObject agent, GameObject target = null)
    {
        currentAgent = agent;
        currentTarget = target;

        foreach(ScriptableAction action in plan.GetActions())
        {
            DisplayPlanConsole(plan);

            Invoke(action.name, 0);
            AnimationHandler.OverrideAnimation(agent, action.Clip, action.state);

            yield return new WaitUntil(() => NextAction == true);
            currentWaitCondition = new WaitCondition() { myBool = false, state = WorldState.playerSeen };
            AnimationHandler.ResetAnimatorTriggers(agent);

        }
        
        //Create a new plan with the goal of finding a new player
        currentAgent.GetComponent<GoapAgent>().ObtainNewPlan(new Goal(WorldState.playerSeen));
    }

    bool ConditionChecker(WaitCondition cond)
    {
        cond.myBool = currentAgent.GetComponent<GoapAgent>().memory.GetMemories().Contains(cond.state);
        return cond.myBool;
    }

    void DisplayPlanConsole(Plan plan)
    {
        foreach(ScriptableAction action in plan.GetActions())
        {
            print(action.name);
        }
    }

    void GoTowards()
    {
        currentWaitCondition = new WaitCondition() { myBool = false, state = WorldState.playerNear };
        print("went towards");
        currentAgent.GetComponent<NavMeshAgent>().SetDestination(currentTarget.transform.position);

    }

    void SearchForPlayer()
    {
        currentWaitCondition = new WaitCondition() { myBool = false, state = WorldState.playerSeen };
        currentAgent.GetComponent<GoapAgent>().searching = true;
    }

    void AttackPlayerMelee()
    {
        currentWaitCondition = new WaitCondition() { myBool = false, state = WorldState.noState };
        currentAgent.GetComponent<GoapAgent>().memory.RemoveMemory(WorldState.playerSeen);
        currentAgent.GetComponent<GoapAgent>().memory.RemoveMemory(WorldState.playerNear);
        print("attacked player with melee");
    }

    void CastAtPlayer()
    {
        print("attacked player with projectile");
    }

    //Defines a condition that the executer should wait to be fullfilled before running the next action
    private struct WaitCondition
    {
        public bool myBool;
        public WorldState state;
    }
}
