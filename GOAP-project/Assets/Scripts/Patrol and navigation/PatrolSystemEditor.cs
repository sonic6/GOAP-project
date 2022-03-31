using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class PatrolSystemEditor : EditorWindow
{
    PatrolWeb web; 

    [MenuItem("Window/PatrolSystemEditor")]
    public static void OpenWindow()
    {
        GetWindow(typeof(PatrolSystemEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Welcome to the patrol system editor!", EditorStyles.centeredGreyMiniLabel);
        GUILayout.Label("Here you can drag and drop a parent object which containts patrol points as children", EditorStyles.boldLabel);
        GUILayout.Label("when you press 'visualize' this editor will draw lines between the patrol points", EditorStyles.boldLabel);
        //AddPatrolWeb();
    }

    //private void AddPatrolWeb()
    //{
    //    web = EditorGUILayout.ObjectField("parent", web, typeof(PatrolWeb), true) as PatrolWeb;

    //    if (GUILayout.Button("Visualize"))
    //    {
    //        web.points = web.GetComponentsInChildren<PatrolPoint>().ToList();
    //    }
    //}
}
