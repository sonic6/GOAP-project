using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CaptivePositions : MonoBehaviour
{
    public List<bool> slots = new List<bool> { false, false, false, }; //false means slot is empty
    public List<Transform> slotsPositions;
    [SerializeField] Vector3 rotationAngle;
    
    public void AddCaptive(GameObject captive)
    {
        foreach(Collider collider in captive.GetComponentsInChildren<Collider>())
        {
            Destroy(collider);
        }

        Destroy(captive.GetComponent<Rigidbody>());
        Destroy(captive.GetComponent<Collider>());
        Destroy(captive.GetComponent<GoapAgent>());
        captive.GetComponent<NavMeshAgent>().enabled = false;

        int i = 0;

        //Find an empty slot
        for(i = 0; i < slots.Count; i++)
        {
            if(!slots[i])
            {
                slots[i] = true;
                break;
            }
        }

        //Rescale the captive
        captive.transform.localScale = captive.transform.localScale/15;


        //Attach the captive to its slot
        captive.transform.position = slotsPositions[i].position;
        captive.transform.parent = slotsPositions[i];
    }

    private void FixedUpdate()
    {
        foreach(Transform orb in slotsPositions)
        {
            orb.Rotate(rotationAngle);
        }
    }
}
