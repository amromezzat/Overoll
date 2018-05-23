using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LanesDatabase))]
public class LaneEditor : Editor
{

    LanesDatabase lanesDatabase;
    bool showEditLanes;

    private void OnEnable()
    {
        lanesDatabase = (LanesDatabase)target;
        showEditLanes = true;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        EditorGUILayout.Separator();

        showEditLanes = EditorGUILayout.Foldout(showEditLanes, "Edit Lanes");
        if (showEditLanes)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add a left lane"))
            {
                lanesDatabase.AddLeft();
            }
            if (GUILayout.Button("Add a right lane"))
            {
                lanesDatabase.AddRight();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove a left lane"))
            {
                lanesDatabase.RemoveLeft();
            }
            if (GUILayout.Button("Remove a right lane"))
            {
                lanesDatabase.RemoveRight();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Lane Width");
            EditorGUILayout.TextArea(lanesDatabase.LaneWidth.ToString());
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Recalculate lanes center"))
            {
                lanesDatabase.RecalculateLanesCenter();
            }
        }

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(lanesDatabase);
    }
}
