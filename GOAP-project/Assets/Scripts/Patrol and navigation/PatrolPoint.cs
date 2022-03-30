using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField] protected float size; //Sets the size of the gizmo 

    private void OnDrawGizmos()
    {
        DrawSphere();
    }

    //Draws a blue sphere using wire sphere gizmos
    public virtual void DrawSphere()
    {
        Gizmos.color = Color.blue; //Sets the color of the gizmo
        Gizmos.DrawWireSphere(transform.position, size); //Creates a gizmo in the scene view
    }

    public virtual void DrawLine(List<PatrolPoint> patrolPoints, int nextPositionIndex)
    {
        Gizmos.color = Color.blue; //Sets the color of the gizmo
        //This try catch block is used to ignore the error caused by the mthod trying to draw a line towards a non-existing point (end of the list)
        try
        {
            Gizmos.DrawLine(transform.position, patrolPoints[nextPositionIndex].transform.position);
        }
        catch { }
        
    }
}
