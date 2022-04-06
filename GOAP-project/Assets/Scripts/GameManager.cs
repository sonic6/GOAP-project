using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlanExecuter))]
public class GameManager : MonoBehaviour
{
    PatrolWeb web;
    [SerializeField] Transform webParent; //The parent gameobject which contains children that have patrol point components

    private void Awake()
    {
        web = new PatrolWeb(webParent);
    }
}
