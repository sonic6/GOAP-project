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

    public void Execute(Plan plan, GameObject agent)
    {
        currentAgent = agent;

        foreach(ScriptableAction action in plan.GetActions())
        {
            Invoke(action.name, 0);
        }
    }

    void GoTowards(NavMeshAgent navAgent, Vector3 location)
    {
        
    }

    void SearchForPlayer()
    {
        NavMeshAgent nav = currentAgent.GetComponent<NavMeshAgent>();
        //Find new destination
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
