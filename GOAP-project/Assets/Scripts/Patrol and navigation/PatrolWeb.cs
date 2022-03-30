using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolWeb : MonoBehaviour
{
    // A patrol web has access to all the patrol points in the level and organizes them so an agent can follow them thoughout the level

    public List<PatrolPoint> points = new List<PatrolPoint>();

    //private void OnValidate()
    //{
    //    foreach (PatrolPoint point in gameObject.GetComponentsInChildren<PatrolPoint>())
    //    {
    //        points.Add(point);
    //    }
    //}

    private void OnDrawGizmos()
    {
        DrawWeb();
    }

    public void DrawWeb()
    {
        for (int i = 0; i < points.Capacity; i++)
        {
            points[i].DrawLine(points, i+1);
        }
    }
}
