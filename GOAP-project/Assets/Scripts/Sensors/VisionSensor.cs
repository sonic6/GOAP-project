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

    RaycastHit hit;

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

            Memory oldFact = new Memory() { state = WorldState.playerSeen, target = other.gameObject };
            agent.memory.RemoveMemory(oldFact, new Goal(WorldState.playerSeen));
        }
    }

    //Casts a line of sight towards a target to make sure it is still seen 
    IEnumerator StareAtTarget(GameObject target)
    {
        Physics.Linecast(eyePosition.position, target.transform.position, out hit);
        Debug.DrawLine(eyePosition.position, target.transform.position);

        //If the target is in the line of sight
        if(hit.collider.gameObject == target)
        {
            Memory newFact = new Memory() { state = WorldState.playerSeen, target = target.gameObject };
            agent.memory.AddMemory(newFact, new Goal(WorldState.playerNear));
        }

        //Keep checking that the target is in the line of sight
        while(FovTarget.Contains(target) && hit.collider.gameObject == target)
        {
            Physics.Linecast(eyePosition.position, target.transform.position, out hit);
            Debug.DrawLine(eyePosition.position, target.transform.position);

            yield return new WaitForSeconds(1f);
        }

        #region remove target from memory and FovList
        Memory oldFact = new Memory() { state = WorldState.playerSeen, target = target.gameObject };
        agent.memory.RemoveMemory(oldFact, new Goal(WorldState.playerSeen));

        FovTarget.Remove(target);
        #endregion


    }

}
