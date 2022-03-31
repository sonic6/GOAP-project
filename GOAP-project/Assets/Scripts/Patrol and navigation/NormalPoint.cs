using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPoint : PatrolPoint
{
    bool logMessage = false;

    //Draws a blue sphere using wire sphere gizmos
    public override void DrawSphere()
    {
        Gizmos.color = Color.blue; //Sets the color of the gizmo
        Gizmos.DrawWireSphere(transform.position, size); //Creates a gizmo in the scene view
    }

    public override void DrawLine(List<PatrolPoint> patrolPoints, int nextPositionIndex, int skip)
    {
        Gizmos.color = Color.blue; //Sets the color of the gizmo
        ////This try catch block is used to ignore the error caused by the mthod trying to draw a line towards a non-existing point (end of the list)
        //try
        //{
        //    Gizmos.DrawLine(transform.position, patrolPoints[nextPositionIndex].transform.position);
        //    patrolPoints[nextPositionIndex].DrawLine(patrolPoints, nextPositionIndex + skip, skip);
        //}
        //catch { }
        try
        {
            Gizmos.DrawLine(transform.position, nextPoint.transform.position);
        }
        catch
        {
            if (!logMessage) 
            {
                print("Please assign " + name + " its next point");
                logMessage = true;
            }
        }
        

    }

    public override void SetNextPoint(List<PatrolPoint> patrolPoints, int nextPositionIndex, int skip)
    {
        try
        {
            //Makes sure a point's next point doesn't skip the convergence point if the conv point is next
            if (nextPoint == null)
            {
                nextPoint = patrolPoints[nextPositionIndex];
                nextPoint.SetNextPoint(patrolPoints, nextPositionIndex + skip, skip);
            }
        }
        catch { }
    }

    public override PatrolPoint GetNextPoint()
    {
        return nextPoint;
    }
}
