using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvergencePoint : PatrolPoint
{
    public override void DrawLine(List<PatrolPoint> patrolPoints, int nextPositionIndex, int skip)
    {
        Gizmos.color = Color.red; //Sets the color of the gizmo
        //This try catch block is used to ignore the error caused by the mthod trying to draw a line towards a non-existing point (end of the list)
        try
        {
            //Draws a line towards the previous point
            Gizmos.DrawLine(transform.position, patrolPoints[nextPositionIndex - skip - 1].transform.position);

            //Draws a line towards the next point
            Gizmos.DrawLine(transform.position, patrolPoints[nextPositionIndex - skip + 1].transform.position);
        }
        catch { }
        
        patrolPoints[nextPositionIndex].DrawLine(patrolPoints, nextPositionIndex);
    }

    public override void DrawSphere()
    {
        Gizmos.color = Color.red; //Sets the color of the gizmo
        Gizmos.DrawWireSphere(transform.position, size); //Creates a gizmo in the scene view
    }

    public override void SetNextPoint(List<PatrolPoint> patrolPoints, int nextPositionIndex, int skip)
    {
        try
        {
            nextPoint = patrolPoints[nextPositionIndex - skip + 1];
            nextPoint.SetNextPoint(patrolPoints, nextPositionIndex + skip);

            //Tell the previous point that its next point is this point
            patrolPoints[nextPositionIndex - skip - 1].nextPoint = this;
        }
        catch { }
    }
}
