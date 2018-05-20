using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LanesDatabase))]
public class LaneEditor : Editor
{

    LanesDatabase lanes;
    bool showEditLanes;

    private void OnEnable()
    {
        lanes = (LanesDatabase)target;
        showEditLanes = true;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Separator();

        showEditLanes = EditorGUILayout.Foldout(showEditLanes, "Edit Lanes");
        if (showEditLanes)
        {
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

            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Lane Width");
            EditorGUILayout.TextArea(lanes.LaneWidth.ToString());
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Recalculate lanes center"))
            {
                lanes.RecalculateLanesCenter();
            }
        }
    }
}
