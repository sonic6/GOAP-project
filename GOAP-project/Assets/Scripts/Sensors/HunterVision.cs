using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterVision : VisionSensor
{

    //Casts a line of sight towards a target to make sure it is still seen 
    //Tells the agent that their target was seen
    public override IEnumerator StareAtTarget(GameObject target)
    {
        
        Physics.Linecast(eyePosition.position, target.transform.position, out hit);
        Debug.DrawLine(eyePosition.position, target.transform.position);

        //If the target is in the line of sight
        if (hit.collider.gameObject == target)
        {
            Memory newFact = new Memory() { state = WorldState.targetSeen, target = target.gameObject };
            agent.memory.AddMemory(newFact, new Goal(WorldState.playerCaptured));
        }
        //else //debugging
        //{
        //    Debug.LogError("target is " + target.name);
        //    Debug.LogError("hit.collider.gameObject is " + hit.collider.gameObject.name);
        //}

        //Keep checking that the target is in the line of sight
        while (FovTarget.Contains(target) && hit.collider != null && hit.collider.gameObject == target)
        {
            Physics.Linecast(eyePosition.position, target.transform.position, out hit);
            Debug.DrawLine(eyePosition.position, target.transform.position);

            yield return new WaitForSeconds(1f);
        }

        #region remove target from memory and FovList
        Memory oldFact = new Memory() { state = WorldState.targetSeen, target = target.gameObject };
        agent.memory.RemoveMemory(oldFact, new Goal(WorldState.targetSeen));

        FovTarget.Remove(target);
        #endregion

        yield return null;
    }
}
