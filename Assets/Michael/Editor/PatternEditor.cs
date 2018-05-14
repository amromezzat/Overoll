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
        
    }
}
