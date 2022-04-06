using System.Collections;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlanExecuter : MonoBehaviour
{
    public static PlanExecuter main;
    private GameObject currentAgent;

    private void Awake()
    {
        main = this;
    }

    public IEnumerator Execute(Plan plan, GameObject agent)
    {
        currentAgent = agent;

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

    void GoTowards(NavMeshAgent navAgent, Vector3 location)
    {
        
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
