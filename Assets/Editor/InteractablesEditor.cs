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
            EditorGUI.indentLevel++;
            for (int i = 0; i < interactablesDB.Count; i++)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(interactablesDB.interactablesNames[i],
                    EditorStyles.helpBox, GUILayout.MaxWidth(200));

                EditorGUI.BeginDisabledGroup(i == 0);
                if (GUILayout.Button("▲", EditorStyles.label))
                {
                    interactablesDB.SwapByIndex(i, i - 1);
                }
                EditorGUI.EndDisabledGroup();

                EditorGUI.BeginDisabledGroup(i == interactablesDB.interactablesNames.Count - 1);
                if (GUILayout.Button("▼", EditorStyles.label))
                {
                    interactablesDB.SwapByIndex(i, i + 1);
                }
                EditorGUI.EndDisabledGroup();
                GUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;
        }

        if (GUI.changed)
        {
            interactablesDB.interactablesNames = new List<string>(interactablesDB.Count);
            for (int i = 0; i < interactablesDB.Count; i++)
            {
                interactablesDB.interactablesNames.Add(interactablesDB[i].name);
            }
        }
    }
}
