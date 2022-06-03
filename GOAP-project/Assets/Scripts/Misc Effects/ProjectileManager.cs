using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] float projectileCoolDown;
    GoapAgent myAgent;

    // Start is called before the first frame update
    void Start()
    {
        myAgent = gameObject.GetComponent<GoapAgent>();

        //Invoking this method too early will cause an error due to the memory not having been instantiated in
        //the GoapAgent yet
        Invoke("GainProjectile", .1f);
    }
    
    //Waits for a predecided amount of seconds then gives the agent a new projectile 
    public IEnumerator RefreshProjectile()
    {
        yield return new WaitForSeconds(projectileCoolDown);
        GainProjectile();
    }

    void GainProjectile()
    {
        Memory mem = new Memory() { state = WorldState.projectileAvailable };
        myAgent.memory.AddMemory(mem, new Goal(WorldState.targetSeen));
    }
}
