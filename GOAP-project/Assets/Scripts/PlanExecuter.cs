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
    //private WaitCondition currentWaitCondition;
    //private bool NextAction = false;

    private void Awake()
    {
        main = this;
    }

    //private void Update()
    //{
    //    NextAction = ConditionChecker(currentWaitCondition);
    //}

    public IEnumerator Execute(Plan plan, GameObject agent, GameObject target = null)
    {
        currentAgent = agent;
        currentTarget = target;

        foreach(ScriptableAction action in plan.GetActions())
        {
            DisplayPlanConsole(plan);

            //currentWaitCondition = new WaitCondition() { myBool = false, fact = new Memory { state = action.effectKey } };
            Invoke(action.name, 0);
            AnimationHandler.OverrideAnimation(agent, action.Clip, action.state);

            //yield return new WaitUntil(() => NextAction == true);
            yield return new WaitUntil(() => ConditionChecker(action) == true);
            //currentWaitCondition = new WaitCondition() { myBool = false };
            AnimationHandler.ResetAnimatorTriggers(agent);

        }
        
        //Create a new plan with the goal of finding a new player
        currentAgent.GetComponent<GoapAgent>().ObtainNewPlan(new Goal(WorldState.playerSeen));
    }

    //Returns true if an agent has fullfilled an action's effect
    //bool ConditionChecker(WaitCondition cond)
    //{
    //    if (currentAgent)
    //    {
    //        cond.myBool = currentAgent.GetComponent<GoapAgent>().memory.ContainsMatchingMemory(cond.fact);
    //        return cond.myBool;
    //    }
    //    return false;
    //}

    //Returns true if an agent has fullfilled an action's effect
    bool ConditionChecker(ScriptableAction action)
    {
        Memory checkMemory = new Memory() { state = action.effectKey, target = currentTarget };
        return currentAgent.GetComponent<GoapAgent>().memory.ContainsMatchingMemory(checkMemory);
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
        print("went towards");
        currentAgent.GetComponent<NavMeshAgent>().SetDestination(currentTarget.transform.position);

    }

    void Patrol()
    {
        StartCoroutine(PatrolUntil());
        currentAgent.GetComponent<GoapAgent>().searching = true;
        
    }

    void AttackPlayerMelee()
    {
        //currentWaitCondition = new WaitCondition() { myBool = false, fact = new Memory { state = WorldState.noState } };
        currentAgent.GetComponent<GoapAgent>().memory.RemoveMemory(new Memory { state = WorldState.playerSeen, target = currentTarget }, new Goal(WorldState.playerSeen));
        currentAgent.GetComponent<GoapAgent>().memory.RemoveMemory(new Memory { state = WorldState.playerNear, target = currentTarget }, new Goal(WorldState.playerSeen));
        print("attacked player with melee");
    }

    void CastAtPlayer()
    {
        print("attacked player with projectile");
    }

    //Defines a condition that the executer should wait to be fullfilled before running the next action
    //private struct WaitCondition
    //{
    //    public bool myBool;
    //    public Memory fact;
    //}

    IEnumerator PatrolUntil()
    {
        print("patrolling");
        NavMeshAgent navAgent = currentAgent.GetComponent<NavMeshAgent>();
        yield return new WaitUntil(() => (Vector3.Distance(currentAgent.transform.position, navAgent.destination) < .5f));
        
        currentAgent.GetComponent<GoapAgent>().destination = currentAgent.GetComponent<GoapAgent>().destination.GetNextPoint();
        navAgent.SetDestination(currentAgent.GetComponent<GoapAgent>().destination.transform.position);

        Memory newFact = new Memory() { state = WorldState.noState };
        currentAgent.GetComponent<GoapAgent>().memory.AddMemory(newFact, new Goal(WorldState.playerSeen));
    }
}
