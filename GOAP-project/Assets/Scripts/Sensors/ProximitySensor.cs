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
        if (other.gameObject.tag.ToLower() == "hunted")
        {
            WorldFact newFact = new WorldFact() { state = WorldState.playerNear, target = other.gameObject };
            myAgent.memory.AddMemory(newFact, new Goal(WorldState.playerCaptured));
        }
    }
}
