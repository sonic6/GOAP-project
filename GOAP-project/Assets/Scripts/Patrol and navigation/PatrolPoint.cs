using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PatrolPoint : MonoBehaviour
{
    [SerializeField] protected float size; //Sets the size of the gizmo 
    public PatrolPoint nextPoint;

    private void OnDrawGizmos()
    {
        DrawSphere();
    }
    
    /// <summary>
    /// Draws a gizmo sphere in the editor
    /// </summary>
    public abstract void DrawSphere();

    /// <summary>
    /// Draws lines between patrol points in the editor
    /// </summary>
    /// <param name="patrolPoints"></param>
    /// <param name="nextPositionIndex"></param>
    /// <param name="skip"></param>
    public abstract void DrawLine(List<PatrolPoint> patrolPoints, int nextPositionIndex, int skip = 1);

    /// <summary>
    /// Determines which point is the next point during gameplay
    /// </summary>
    /// <param name="patrolPoints"></param>
    /// <param name="nextPositionIndex"></param>
    /// <param name="skip"></param>
    public abstract void SetNextPoint(List<PatrolPoint> patrolPoints, int nextPositionIndex, int skip = 1);
}
