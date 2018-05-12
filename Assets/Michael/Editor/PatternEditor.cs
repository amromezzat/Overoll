using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// This script is used to modify obstacles in patterns segement by segment
/// </summary>

//[CustomEditor(typeof(Pattern))]
//[CustomEditor(typeof(CoinGenerator))]
public class PatternEditor : Editor
{
    public int noOfSegments;

    public override void OnInspectorGUI()
    {
        // call some function here to genereate <noOfSegments> segment(s)
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("far left lane"))
        {
            // call the functuin of far left lane instantiation
        }

        if (GUILayout.Button("left lane"))
        {
            // call the functuin of left lane instantiation

        }

        if (GUILayout.Button("Middle Lane"))
        {
            // call the functuin of middle instantiation

        }

        if (GUILayout.Button("Right Lane"))
        {
            // call the functuin of Right lane instantiation

        }

        if (GUILayout.Button("far Righy Lane"))
        {
            // call the functuin of far Right lane instantiation

        }

        GUILayout.EndHorizontal();
    }
}
