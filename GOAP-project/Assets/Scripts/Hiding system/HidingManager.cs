using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingManager : MonoBehaviour
{
    public Room currentRoom { get; private set; }
    GoapAgent myAgent;

    private void Awake()
    {
        myAgent = GetComponent<GoapAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        EnteredRoomCheck(other);
        InHidingSpotCheck(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Room>() == currentRoom)
        {
            currentRoom = null;
        }
    }

    void EnteredRoomCheck(Collider other)
    {
        if (other.gameObject.GetComponent<Room>())
        {
            currentRoom = other.gameObject.GetComponent<Room>();
        }
    }

    void InHidingSpotCheck(Collider other)
    {
        if (other.gameObject.GetComponent<HidingSpot>())
        {
            print("triggered by hiding spot");
            Memory mem = new Memory()
            {
                state = WorldState.IsHiding 
            };
            myAgent.memory.AddMemory(mem, new Goal(WorldState.targetSeen));
        }
    }
}
