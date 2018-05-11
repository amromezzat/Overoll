using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Pattern))]

[CustomEditor(typeof(CoinPool))]
public class PatternEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Button("hamada");
    }
}
