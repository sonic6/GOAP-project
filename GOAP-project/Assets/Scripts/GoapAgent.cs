using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoapAgent : MonoBehaviour
{
    public List<WorldState> debugDisplayMemories = new List<WorldState>(); //Debugging
    public List<ScriptableAction> DebugDisplayPlan = new List<ScriptableAction>(); //Debugging

    public WorkingMemory memory { get; private set; }
    [SerializeField] ActionSet agentActionSet;
    [HideInInspector] public Planner planner;
    private NavMeshAgent navAgent;
    [HideInInspector] public PatrolPoint destination;
    [HideInInspector] public bool searching = false;

    [Tooltip("Add a memory for this agent to start with. Other than the noState memory")]
    [SerializeField] WorldState firstMemory;
    
    public AnimatorOverrideController overrider;

    
    private void Start()
    {
        SetUpAgent();
    }

    private void SetUpAgent()
    {
        navAgent = GetComponent<NavMeshAgent>();
        destination = NearestPatrolPoint();
        memory = new WorkingMemory(this);
        planner = new Planner(this);
        navAgent.SetDestination(destination.transform.position);

        Memory firstFact = new Memory() { state = firstMemory };
        memory.AddMemory(firstFact, new Goal(WorldState.playerSeen));
    }

    private PatrolPoint NearestPatrolPoint()
    {
        //Find closest patrol point
        PatrolPoint closestPoint = PatrolWeb.main.points[0];
        foreach (PatrolPoint point in PatrolWeb.main.points)
        {
            if (Vector3.Distance(point.transform.position, transform.position) < Vector3.Distance(closestPoint.transform.position, transform.position))
            {
                closestPoint = point;
            }
        }
        return closestPoint;
    }

    public Plan ObtainNewPlan(Goal goal)
    {
        Plan newPlan = planner.CreatePlan(goal, agentActionSet);
        DebugDisplayPlan = newPlan.GetActions();
        return newPlan;
    }

    public void ExecutePlan(Plan plan, GameObject target = null)
    {
        StartCoroutine(PlanExecuter.main.Execute(plan, gameObject, target));
    }

    public void DebugMove()
    {
        if (searching && Vector3.Distance(transform.position, navAgent.destination) < .5f)
        {
            navAgent.SetDestination(destination.transform.position);
            destination = destination.GetNextPoint();
        }
    }

    private void DebugDisplayMemories()
    {
        //debugDisplayMemories = memory.GetMemories();
        List<WorldState> debugStates = new List<WorldState>();

        foreach(Memory fact in memory.GetMemories())
        {
            debugStates.Add(fact.state);
        }
        debugDisplayMemories = debugStates;
    }

    private void Update()
    {
        DebugMove();
        DebugDisplayMemories();
    }
}

