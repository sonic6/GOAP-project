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
            
            WorldFact newFact = new WorldFact() { state = WorldState.playerSeen, target = other.gameObject };
            agent.memory.AddMemory(newFact, new Goal(WorldState.playerNear));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.ToLower() == "hunted")
        {
            FovTarget = null;

            WorldFact oldFact = new WorldFact() { state = WorldState.playerSeen, target = other.gameObject };
            agent.memory.RemoveMemory(oldFact, new Goal(WorldState.playerSeen));
        }
    }

    private void LineOfSight(Transform target)
    {
        
        Debug.DrawLine(eyePosition.position, target.position, Color.red);
        if (Physics.Linecast(eyePosition.position, target.position, out hit) && hit.collider.gameObject.tag == "hunted")
        {
            print("player in fov");
            navAgent.SetDestination(target.transform.position);
        }
    }

    private void FixedUpdate()
    {
        if(FovTarget != null)
        {
            LineOfSight(FovTarget.transform);
        }
    }

}
