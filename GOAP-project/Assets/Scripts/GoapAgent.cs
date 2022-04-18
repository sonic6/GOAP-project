using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
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

    [Tooltip("Add a memory for this agent to start with. Other than the noState memory")]
    [SerializeField] WorldState firstMemory;

    //[SerializeField] Animator animator;
    public AnimatorOverrideController overrider;

    //List<AnimationClip> clips;

    //Sensors
    public ProximitySensor proximity;
    

    private void Start()
    {
        SetUpAgent();
        Goal firstGoal = new Goal(WorldState.playerSeen);
        Plan firstPlan = ObtainNewPlan(firstGoal);
        ExecutePlan(firstPlan, gameObject);
    }

    private void SetUpAgent()
    {
        navAgent = GetComponent<NavMeshAgent>();
        destination = NearestPatrolPoint();
        memory = new WorkingMemory();
        planner = new Planner(this);
        proximity = new ProximitySensor(this);
        navAgent.SetDestination(destination.transform.position);

        AddMemory(firstMemory);
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

    public void ExecutePlan(Plan plan, GameObject target)
    {
        StartCoroutine(PlanExecuter.main.Execute(plan, gameObject, target));
    }


    public void Animate(AnimationClip clip)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.ToLower() == "hunted")
        {
            navAgent.SetDestination(transform.position); //Stops teh navmesh agent from going towards the destination from a previous plan
            memory.states.Add(WorldState.playerSeen);
            Plan myPlan = ObtainNewPlan(new Goal(WorldState.playerCaptured));
            ExecutePlan(myPlan, other.gameObject);
        }
    }

    
    public void AddMemory(WorldState newMemory)
    {
        memory.states.Add(newMemory);
    }

    public void DebugMove()
    {
        if (Vector3.Distance(transform.position, navAgent.destination) < .5f)
        {
            navAgent.SetDestination(destination.transform.position);
            destination = destination.GetNextPoint();
        }
    } 

    private void DebugDisplayMemories()
    {
        debugDisplayMemories = memory.states;
    }

    private void Update()
    {
        DebugDisplayMemories();
        proximity.IfNearTarget();
    }


}
