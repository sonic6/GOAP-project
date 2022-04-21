using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProximitySensor : MonoBehaviour
{
    GoapAgent myAgent;
    //public bool chase = false; //means that the agent is chasing the target

    private void Awake()
    {
        myAgent = GetComponentInParent<GoapAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.ToLower() == "hunted")
        {
            Debug.Log("dsdsd");
            myAgent.memory.AddMemory(WorldState.playerNear, new Goal(WorldState.playerCaptured), other.gameObject);
        }
    }
}
