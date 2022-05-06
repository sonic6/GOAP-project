using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntedVision : VisionSensor
{
    //Casts a line of sight towards a target to make sure it is still seen 
    //Tells the agent where to hide
    public override IEnumerator StareAtTarget(GameObject target)
    {
        Physics.Linecast(eyePosition.position, target.transform.position, out hit);
        Debug.DrawLine(eyePosition.position, target.transform.position);

        //If the target is in the line of sight
        if (hit.collider.gameObject == target)
        {
            print(gameObject.name + " sees hunter " + target.name);
            HidingSpot spot = null;

            foreach(Room room in GameManager.allRooms)
            {
                if(room.huntersInRoom.Count == 0)
                {
                    spot = room.GetHidingSpot();
                    spot.occupied = true;
                }
            }

            Memory newFact = new Memory() { state = WorldState.targetSeen, target = spot.gameObject };
            agent.memory.AddMemory(newFact, new Goal(WorldState.IsHiding));
        }

        

        yield return null;
    }

    //Check if the agent sees a key
    public override void SeeKey(GameObject key)
    {
        Physics.Linecast(eyePosition.position, key.transform.position, out hit);
        Debug.DrawLine(eyePosition.position, key.transform.position);

        if (hit.collider.gameObject == key)
        {
            print("sees key");
            Memory newFact = new Memory() { state = WorldState.targetSeen, target = hit.collider.gameObject };
            agent.memory.AddMemory(newFact, new Goal(WorldState.targetNear));
        }
    }
}
