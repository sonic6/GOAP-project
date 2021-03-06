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
            yield return new WaitUntil(() => ConditionChecker(action) == true);

        }
    }

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
            print(currentAgent.name + " is doing " + action.name);
        }
    }

    void GoTowards()
    {
        currentAgent.GetComponent<Animator>().SetTrigger("GoTo");
        currentAgent.GetComponent<NavMeshAgent>().SetDestination(currentTarget.transform.position);
    }

    void Patrol()
    {
        StartCoroutine(PatrolUntil());
    }

    void AttackPlayerMelee()
    {
        currentAgent.GetComponent<Animator>().SetTrigger("melee");
        currentAgent.GetComponent<GoapAgent>().memory.RemoveMemory(new Memory { state = WorldState.targetSeen, target = currentTarget }, new Goal(WorldState.targetSeen));
        currentAgent.GetComponent<GoapAgent>().memory.RemoveMemory(new Memory { state = WorldState.targetNear, target = currentTarget }, new Goal(WorldState.targetSeen));
        print("attacked player with melee");
        GameManager.manager.huntedPlayers.Remove(currentTarget.GetComponent<GoapAgent>());
        currentAgent.GetComponent<CaptivePositions>().AddCaptive(currentTarget);

        
    }

    void CastAtPlayer()
    {
        GoapAgent agent = currentAgent.GetComponent<GoapAgent>();

        //This loop extracts the target object from the memory of targetSeen
        foreach(Memory memory in currentAgent.GetComponent<GoapAgent>().memory.GetMemories())
        {
            if(memory.state == WorldState.targetSeen)
            {
                currentTarget = memory.target;
                break;
            }
        }

        currentAgent.GetComponent<Animator>().SetTrigger("projectile");
        
        print(currentTarget);
        GameManager.manager.huntedPlayers.Remove(currentTarget.GetComponent<GoapAgent>());
        currentAgent.GetComponent<CaptivePositions>().AddCaptive(currentTarget);

        StartCoroutine(agent.GetComponent<ProjectileManager>().RefreshProjectile());

        currentAgent.GetComponent<GoapAgent>().memory.RemoveMemory(new Memory { state = WorldState.targetSeen, target = currentTarget }, new Goal(WorldState.targetSeen));
        Memory mem = new Memory() { state = WorldState.projectileAvailable };
        agent.memory.RemoveMemory(mem, new Goal(WorldState.targetSeen));
    }

    IEnumerator PatrolUntil()
    {
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

    void GrabKey()
    {
        currentAgent.GetComponent<Animator>().SetTrigger("key");
        Memory newFact = new Memory() { state = WorldState.GrabbedKey, target = currentTarget };
        currentAgent.GetComponent<GoapAgent>().memory.AddMemory(newFact, new Goal(WorldState.targetSeen));

        currentTarget.GetComponent<Key>().GetTaken();
    }

    void EscapeCastle()
    {
        Memory newFact = new Memory() { state = WorldState.targetSeen, target = GameManager.manager.escapeDoor };
        currentAgent.GetComponent<GoapAgent>().memory.AddMemory(newFact, new Goal(WorldState.targetNear));
    }
}
