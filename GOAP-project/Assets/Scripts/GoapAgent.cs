using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoapAgent : MonoBehaviour
{
    PlanExecuter executer;

    public List<WorldState> debugDisplayMemories = new List<WorldState>(); //Debugging
    public List<ScriptableAction> DebugDisplayPlan = new List<ScriptableAction>(); //Debugging

    public WorkingMemory memory { get; private set; }
    [SerializeField] ActionSet agentActionSet;
    [HideInInspector] public Planner planner;
    private NavMeshAgent navAgent;
    [HideInInspector] public PatrolPoint destination;
    

    [Tooltip("What is the tag of this charachter's enemy type?")]
    public string enemyTag;

    [Tooltip("Add a memory for this agent to start with. Other than the noState memory")]
    [SerializeField] WorldState firstMemory;
    
    public AnimatorOverrideController overrider;

    
    private void Start()
    {
        SetUpAgent();
    }

    private void SetUpAgent()
    {
        executer = gameObject.AddComponent<PlanExecuter>();
        navAgent = GetComponent<NavMeshAgent>();
        destination = NearestPatrolPoint();
        memory = new WorkingMemory(this);
        planner = new Planner(this);
        navAgent.SetDestination(destination.transform.position);

        Memory firstFact = new Memory() { state = firstMemory };
        memory.AddMemory(firstFact, new Goal(WorldState.targetSeen));
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
        StartCoroutine(executer.Execute(plan, gameObject, target));
    }

    private void DebugDisplayMemories()
    {
        List<WorldState> debugStates = new List<WorldState>();

        foreach(Memory fact in memory.GetMemories())
        {
            debugStates.Add(fact.state);
        }
        debugDisplayMemories = debugStates;
    }

    private void Update()
    {
        DebugDisplayMemories();
    }
}

