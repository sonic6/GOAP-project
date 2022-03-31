using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PatrolWeb : MonoBehaviour
{
    // A patrol web has access to all the patrol points in the level and organizes them so an agent can follow them thoughout the level

    public List<PatrolPoint> points = new List<PatrolPoint>();

    private void Awake()
    {
        points = GetComponentsInChildren<PatrolPoint>().ToList();
        SetSpots();
    }

    private void OnDrawGizmos()
    {
        DrawWeb();
    }

    public void DrawWeb()
    {
        //for (int i = 0; i < points.Capacity; i++)
        //{
        //    points[i].DrawLine(points, i+1);
        //}

        points[0].DrawLine(points, 1);
    }

    private void SetSpots()
    {
        points[0].SetNextPoint(points, 1);
    }
}
