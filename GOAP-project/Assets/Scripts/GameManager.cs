using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlanExecuter))]
public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    //Thew web of patrol points 
    PatrolWeb web;

    //The parent gameobject which contains children that have patrol point components
    [SerializeField] Transform webParent;

    //List of all Rooms where hunted can hide 
    public static List<Room> allRooms;

    //A text mesh pro refrence to on screen text for number of keys collected 
    public TMP_Text keyCounter;

    //List of all hunted agents
    public List<GoapAgent> huntedPlayers;

    //A reference to the door with trigger box which hunted will escape through
    public GameObject escapeDoor;

    //Amount of keys in-game
    public int keyAmount;

    private void Awake()
    {
        manager = this;
        web = new PatrolWeb(webParent);

        allRooms = FindObjectsOfType<Room>().ToList();
    }
}
