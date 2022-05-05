using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public bool occupied = false;

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "hunted")
        {
            occupied = false;
        }
    }
}
