using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// This script is used to modify obstacles in patterns segement by segment
/// </summary>

[CustomEditor(typeof(PatternSO))]
public class PatternEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Save"))
            {
                    
            }
        }
        GUILayout.EndHorizontal();
    }

    void CreatePattern()
    {
        // Create ?

    }
}
