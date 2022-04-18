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

    private void Awake()
    {
        main = this;
    }

    public IEnumerator Execute(Plan plan, GameObject agent, GameObject target)
    {
        currentAgent = agent;
        currentTarget = target;

        foreach(ScriptableAction action in plan.GetActions())
        {
            DisplayPlanConsole(plan);

            Invoke(action.name, 0);
            AnimationHandler.OverrideAnimation(agent, action.Clip, action.state);
            yield return new WaitForSeconds(action.Clip.length);
            //agent.GetComponent<Animator>().ResetTrigger(action.state.ToString());
            AnimationHandler.ResetAnimatorTriggers(agent);
        }
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
        //Implement a Wait until target is nearby function 

        currentAgent.GetComponent<GoapAgent>().memory.states.Add(WorldState.playerNear);

    }

    void SearchForPlayer()
    {
        currentAgent.GetComponent<GoapAgent>().proximity.searching = true;
    }

    void CapturePlayerMelee()
    {
        print("attacked player with melee");
    }

    void CastAtPlayer()
    {
        print("attacked player with projectile");
    }

    void CapturePlayerProjectile()
    {
        print("Captured player using projectile");
    }
}
