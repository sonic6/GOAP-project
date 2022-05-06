using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProximitySensor : MonoBehaviour
{
    GoapAgent myAgent;

    private void Awake()
    {
        myAgent = GetComponentInParent<GoapAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        HunterNearHunted(other);
        HuntedNearKey(other);
    }

    void HunterNearHunted(Collider other)
    {
        if (other.gameObject.tag.ToLower() == myAgent.enemyTag)
        {
            print("close to player ");
            Memory newFact = new Memory() { state = WorldState.targetNear, target = other.gameObject };
            myAgent.memory.AddMemory(newFact, new Goal(WorldState.playerCaptured));
        }
    }

    void HuntedNearKey(Collider other)
    {
        if(other.gameObject.tag == "key")
        {
            
            Memory newFact = new Memory() { state = WorldState.targetNear, target = other.gameObject };
            myAgent.memory.AddMemory(newFact, new Goal(WorldState.GrabbedKey));
        }
    }
}
