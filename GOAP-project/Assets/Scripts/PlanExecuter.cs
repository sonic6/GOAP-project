using System.Collections;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlanExecuter : MonoBehaviour
{
    private GameObject currentAgent; //The agent whose plan will be executed
    private GameObject currentTarget; //The gameobject that is targeted by an action

    public IEnumerator Execute(Plan plan, GameObject agent, GameObject target = null)
    {
        currentAgent = agent;
        currentTarget = target;

        foreach(ScriptableAction action in plan.GetActions())
        {
            DisplayPlanConsole(plan);
            
            Invoke(action.name, 0);
            AnimationHandler.OverrideAnimation(agent, action.Clip, action.state);
            
            yield return new WaitUntil(() => ConditionChecker(action) == true);
            print("finished a plan");
            AnimationHandler.ResetAnimatorTriggers(agent);

        }
    }

    //Returns true if an agent has fullfilled an action's effect
    bool ConditionChecker(ScriptableAction action)
    {
        print(currentAgent.name + " is checking condition " + action.effectKey + " about target " + currentTarget);
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
    }

    void AttackPlayerMelee()
    {
        //currentWaitCondition = new WaitCondition() { myBool = false, fact = new Memory { state = WorldState.noState } };
        currentAgent.GetComponent<GoapAgent>().memory.RemoveMemory(new Memory { state = WorldState.targetSeen, target = currentTarget }, new Goal(WorldState.targetSeen));
        currentAgent.GetComponent<GoapAgent>().memory.RemoveMemory(new Memory { state = WorldState.targetNear, target = currentTarget }, new Goal(WorldState.targetSeen));
        print("attacked player with melee");
    }

    void CastAtPlayer()
    {
        print("attacked player with projectile");
    }

    IEnumerator PatrolUntil()
    {
        print("patrolling");
        NavMeshAgent navAgent = currentAgent.GetComponent<NavMeshAgent>();
        yield return new WaitUntil(() => (Vector3.Distance(currentAgent.transform.position, navAgent.destination) < .5f));
        
        currentAgent.GetComponent<GoapAgent>().destination = currentAgent.GetComponent<GoapAgent>().destination.GetNextPoint();
        navAgent.SetDestination(currentAgent.GetComponent<GoapAgent>().destination.transform.position);

        Memory newFact = new Memory() { state = WorldState.noState };
        currentAgent.GetComponent<GoapAgent>().memory.AddMemory(newFact, new Goal(WorldState.targetSeen));
    }

    void Hide()
    {
        StartCoroutine(HideUntil());
    }

    IEnumerator HideUntil()
    {
        Room room = currentAgent.GetComponent<HidingManager>().currentRoom;
        NavMeshAgent navAgent = currentAgent.GetComponent<NavMeshAgent>();
        navAgent.SetDestination(currentTarget.transform.position);
        yield return new WaitUntil(() => (Vector3.Distance(currentAgent.transform.position, navAgent.destination) < .01f));
        print(room);
        yield return new WaitUntil(() => (room.huntersInRoom.Count == 0));
        print("finished hiding");

        Memory oldMemory = new Memory()
        {
            state = WorldState.IsHiding
        };
        currentAgent.GetComponent<GoapAgent>().memory.RemoveMemory(oldMemory, new Goal(WorldState.targetSeen));
    }
}
