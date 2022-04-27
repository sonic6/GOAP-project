using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    List<HidingSpot> hidingSpots = new List<HidingSpot>();

    private void Awake()
    {
        hidingSpots = transform.GetComponentsInChildren<HidingSpot>().ToList();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
