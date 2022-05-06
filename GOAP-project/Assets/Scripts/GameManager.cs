using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlanExecuter))]
public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    PatrolWeb web;
    [SerializeField] Transform webParent; //The parent gameobject which contains children that have patrol point components
    public static List<Room> allRooms;
    public TMP_Text keyCounter;
    public List<GoapAgent> huntedPlayers;
    public GameObject escapeDoor;

    private void Awake()
    {
        manager = this;
        web = new PatrolWeb(webParent);

        allRooms = FindObjectsOfType<Room>().ToList();
    }
}
