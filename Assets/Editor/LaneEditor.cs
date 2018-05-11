using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Lanes))]
public class LaneEditor : Editor {

    Lanes lanes;

    private void OnEnable()
    {
        lanes = (Lanes)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Separator();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Edit Lanes");

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add a left lane"))
        {
            lanes.AddLeft();
        }
        if (GUILayout.Button("Add a right lane"))
        {
            lanes.AddRight();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Remove a left lane"))
        {
            lanes.RemoveLeft();
        }
        if (GUILayout.Button("Remove a right lane"))
        {
            lanes.RemoveRight();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        if(GUILayout.Button("Recalculate lanes center")){
            lanes.RecalculateLanesCenter();
        }
    }
}
