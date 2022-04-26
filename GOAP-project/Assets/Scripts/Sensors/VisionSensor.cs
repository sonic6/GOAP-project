using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisionSensor : MonoBehaviour
{
    NavMeshAgent navAgent;
    GoapAgent agent;

    [SerializeField] Transform eyePosition;

    GameObject FovTarget;
    RaycastHit hit;
    RaycastHit reflect;

    private void Awake()
    {
        agent = GetComponentInParent<GoapAgent>();
        navAgent = agent.gameObject.GetComponent<NavMeshAgent>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.ToLower() == "hunted")
        {
            FovTarget = other.gameObject;
            
            Memory newFact = new Memory() { state = WorldState.playerSeen, target = other.gameObject };
            agent.memory.AddMemory(newFact, new Goal(WorldState.playerNear));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.ToLower() == "hunted")
        {
            FovTarget = null;

            Memory oldFact = new Memory() { state = WorldState.playerSeen, target = other.gameObject };
            agent.memory.RemoveMemory(oldFact, new Goal(WorldState.playerSeen));
        }
    }

    private void LineOfSight(Transform target)
    {
        
        Debug.DrawLine(eyePosition.position, target.position, Color.red);
        Physics.Linecast(eyePosition.position, target.position, out hit);
        if (hit.collider.gameObject.tag == "hunted")
        {
            print("player in fov");
            navAgent.SetDestination(target.transform.position);
        }
    }

    //Reflects the line of sight back towards this player to check if line is still viable 
    private void LineOfSightReflection()
    {
        Debug.DrawLine(FovTarget.transform.position, eyePosition.position, Color.yellow);
        Physics.Linecast(FovTarget.transform.position, eyePosition.position, out reflect);

        //print(reflect.collider);
        try
        {
            if (reflect.collider.gameObject != agent.gameObject)
            {
                Memory oldFact = new Memory() { state = WorldState.playerSeen, target = FovTarget.gameObject };
                agent.memory.RemoveMemory(oldFact, new Goal(WorldState.playerSeen));

                FovTarget = null;
            }
        }
        catch { }
        
    }

    private void FixedUpdate()
    {
        if(FovTarget != null)
        {
            LineOfSight(FovTarget.transform);
            LineOfSightReflection();
        }

    }

}
