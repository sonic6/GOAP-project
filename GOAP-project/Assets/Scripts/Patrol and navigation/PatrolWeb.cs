using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PatrolWeb
{
    public static PatrolWeb main;
    public List<PatrolPoint> points = new List<PatrolPoint>();
    
    //Finds all the patrol points that are children to 'parent'
    public void FindPoints(Transform parent)
    {
        main = this;
        points = parent.GetComponentsInChildren<PatrolPoint>().ToList();
    }
}
