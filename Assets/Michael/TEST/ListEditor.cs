using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SegementsList))]
public class ListEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            CreateList();
        }
    }

    void CreateList()
    {
        PatternSO instance = (PatternSO)ScriptableObject.CreateInstance("PatternSO");

        SegementsList classList = (SegementsList)target;

        for (int i = 0; i < classList.list.Count; i++)
        {
            instance.SegmentList[i] = classList.list[i];
        }
    }
}



