using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class VisionSensor : MonoBehaviour
{
    protected NavMeshAgent navAgent;
    protected GoapAgent agent;

    [SerializeField] protected Transform eyePosition;

    protected List<GameObject> FovTarget = new List<GameObject>();

    protected RaycastHit hit;

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
            StartCoroutine(StareAtTarget(other.gameObject));
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.ToLower() == agent.enemyTag)
        {
            FovTarget.Remove(other.gameObject);

            Memory oldFact = new Memory() { state = WorldState.targetSeen, target = other.gameObject };
            agent.memory.RemoveMemory(oldFact, new Goal(WorldState.targetSeen));
        }
    }

    //Casts a line of sight towards a target to make sure it is still seen 
    public abstract IEnumerator StareAtTarget(GameObject target);

}
