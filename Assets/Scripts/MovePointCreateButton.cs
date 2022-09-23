using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(CreateMovePoint))]
public class MovePointCreateButton : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CreateMovePoint createMovePoint = (CreateMovePoint)target;
        if (GUILayout.Button("SaveMovePoint"))
        {
            createMovePoint.AddMovePoint();
        }
    }
}
