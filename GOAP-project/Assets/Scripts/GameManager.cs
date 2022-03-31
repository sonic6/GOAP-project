using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PatrolWeb web = new PatrolWeb();
    [SerializeField] Transform webParent; //The parent gameobject which contains children that have patrol point components

    private void Awake()
    {
        web.FindPoints(webParent);
    }
}
