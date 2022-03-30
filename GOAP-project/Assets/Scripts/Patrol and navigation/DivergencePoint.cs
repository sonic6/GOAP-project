using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivergencePoint : PatrolPoint
{
    [SerializeField] int paths;

    public override void DrawSphere()
    {
        Gizmos.color = Color.green; //Sets the color of the gizmo
        Gizmos.DrawWireSphere(transform.position, size); //Creates a gizmo in the scene view
    }

    public override void DrawLine(List<PatrolPoint> patrolPoints, int nextPositionIndex)
    {
        for(int i = 0; i < paths; i++)
        {
            Gizmos.color = Color.green; //Sets the color of the gizmo
            //This try catch block is used to ignore the error caused by the mthod trying to draw a line towards a non-existing point (end of the list)
            try
            {
                Gizmos.DrawLine(transform.position, patrolPoints[nextPositionIndex + i].transform.position);
            }
            catch { }
        }
    }
}
