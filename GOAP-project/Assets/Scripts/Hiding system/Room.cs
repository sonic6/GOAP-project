using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<HidingSpot> hidingSpots = new List<HidingSpot>();
    public List<GameObject> huntersInRoom { get; private set; }

    private void Awake()
    {
        hidingSpots = transform.GetComponentsInChildren<HidingSpot>().ToList();
        huntersInRoom = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "hunter")
        {
            huntersInRoom.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (huntersInRoom.Contains(other.gameObject))
        {
            huntersInRoom.Remove(other.gameObject);
        }
    }

    //Finds a hiding spot that isn't occupied
    public HidingSpot GetHidingSpot()
    {
        foreach(HidingSpot spot in hidingSpots)
        {
            if (spot.occupied == false)
            {
                print("made it");
                return spot;
            }
        }

        return null;
    }
}
