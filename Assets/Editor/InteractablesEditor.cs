using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InteractablesDatabase))]
public class InteractablesEditor : Editor
{
    InteractablesDatabase interactablesDB;
    bool ShowInteractablesNames = true;

    private void OnEnable()
    {
        interactablesDB = (InteractablesDatabase)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ShowInteractablesNames = EditorGUILayout.Foldout(ShowInteractablesNames, "Interactables Names");
        if (ShowInteractablesNames)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.indentLevel++;
            for (int i = 0; i < interactablesDB.interactablesNames.Count; i++)
            {
                EditorGUILayout.LabelField(interactablesDB.interactablesNames[i]);
            }
            EditorGUI.indentLevel--;
            EditorGUI.EndDisabledGroup();
        }

        if (GUI.changed)
        {
            interactablesDB.interactablesNames = new List<string>(interactablesDB.Count);
            for(int i = 0; i < interactablesDB.Count; i++)
            {
                interactablesDB.interactablesNames.Add(interactablesDB[i].name);
            }
        }
    }
}
