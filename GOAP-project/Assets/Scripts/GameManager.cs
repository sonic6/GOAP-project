using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlanExecuter))]
public class GameManager : MonoBehaviour
{
    PatrolWeb web;
    [SerializeField] Transform webParent; //The parent gameobject which contains children that have patrol point components

    public static List<Room> allRooms;

    private void Awake()
    {
        web = new PatrolWeb(webParent);

        allRooms = FindObjectsOfType<Room>().ToList();
    }
}
