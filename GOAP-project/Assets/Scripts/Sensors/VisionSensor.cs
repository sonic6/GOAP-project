using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisionSensor : MonoBehaviour
{
    NavMeshAgent navAgent;
    GoapAgent agent;

    [SerializeField] Transform eyePosition;

    List<GameObject> FovTarget = new List<GameObject>();
    List<GameObject> Removables = new List<GameObject>(); //Needs to be used to avoid bugs that happen when changing the FovTarget list from inside a foreach loop 

    RaycastHit hit;
    RaycastHit reflect;

    private void Awake()
    {
        agent = GetComponentInParent<GoapAgent>();
        navAgent = agent.gameObject.GetComponent<NavMeshAgent>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.ToLower() == agent.enemyTag)
        {
            FovTarget.Add(other.gameObject);

            LineOfSight();
            
            Memory newFact = new Memory() { state = WorldState.playerSeen, target = other.gameObject };
            agent.memory.AddMemory(newFact, new Goal(WorldState.playerNear));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.ToLower() == agent.enemyTag)
        {
            FovTarget.Remove(other.gameObject);

            Memory oldFact = new Memory() { state = WorldState.playerSeen, target = other.gameObject };
            agent.memory.RemoveMemory(oldFact, new Goal(WorldState.playerSeen));
        }
    }

    //casts a line ray from this agent towards its target to check if there are no obstacles between
    private void LineOfSight()
    {
        foreach (GameObject target in FovTarget)
        {
            Debug.DrawLine(eyePosition.position, target.transform.position, Color.red);
            Physics.Linecast(eyePosition.position, target.transform.position, out hit);
            if (hit.collider.gameObject.tag == agent.enemyTag)
            {
                print("player in fov");
                navAgent.SetDestination(NearestFovTarget().position);
            }
        }
        
    }

    //Reflects the line of sight back towards this player to check if line is still viable 
    private void LineOfSightReflection()
    {
        foreach(GameObject target in FovTarget)
        {
            Debug.DrawLine(target.transform.position, eyePosition.position, Color.yellow);
            Physics.Linecast(target.transform.position, eyePosition.position, out reflect);

            try
            {
                if (reflect.collider.gameObject != agent.gameObject)
                {
                    Memory oldFact = new Memory() { state = WorldState.playerSeen, target = target.gameObject };
                    agent.memory.RemoveMemory(oldFact, new Goal(WorldState.playerSeen));

                    Removables.Add(target);
                }
            }
            catch { }
        }

        RemoveRemovables();
        Removables.Clear();
    }

    private Transform NearestFovTarget()
    {
        Transform nearest = FovTarget[0].transform;

        foreach (GameObject target in FovTarget)
        {
            if(Vector3.Distance(transform.position, target.transform.position) < Vector3.Distance(transform.position, nearest.position))
            {
                nearest = target.transform;
            }
        }

        return nearest;
    }

    private void RemoveRemovables()
    {
        foreach(GameObject removable in Removables)
        {
            FovTarget.Remove(removable);
        }
    }

    private void FixedUpdate()
    {
        //LineOfSight();
        //LineOfSightReflection();

    }

}
