using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivergencePoint : PatrolPoint
{
    [SerializeField] int paths;
    [SerializeField] List<PatrolPoint> nextPoints;

    public override void DrawSphere()
    {
        Gizmos.color = Color.green; //Sets the color of the gizmo
        Gizmos.DrawWireSphere(transform.position, size); //Creates a gizmo in the scene view
    }

    public override void DrawLine(List<PatrolPoint> patrolPoints, int nextPositionIndex, int skip)
    {
        //for(int i = 0; i < paths; i++)
        //{
        //    Gizmos.color = Color.green; //Sets the color of the gizmo
        //    //This try catch block is used to ignore the error caused by the mthod trying to draw a line towards a non-existing point (end of the list)
        //    try
        //    {
        //        int next = nextPositionIndex + i + paths;
        //        Gizmos.DrawLine(transform.position, patrolPoints[nextPositionIndex + i].transform.position);

        //        patrolPoints[nextPositionIndex + i].DrawLine(patrolPoints, next, paths);
        //    }
        //    catch { }


        //}

        try
        {
            foreach (PatrolPoint point in nextPoints)
            {
                Gizmos.DrawLine(transform.position, point.transform.position);
            }
        }
        catch
        {
            print("Please assign " + name + " its next points");
        }
        
    }

    public override void SetNextPoint(List<PatrolPoint> patrolPoints, int nextPositionIndex, int skip)
    {
        for (int i = 0; i < paths; i++)
        {
            try
            {
                nextPoints.Add(patrolPoints[nextPositionIndex + i]);
                nextPoints[i].SetNextPoint(patrolPoints, nextPositionIndex + i + paths, paths);
            }
            catch { }

            
        }
    }

    public override PatrolPoint GetNextPoint()
    {
        return nextPoints[Random.Range(0, nextPoints.Capacity)];
    }
}
