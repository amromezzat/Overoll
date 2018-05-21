﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor to show and Delete the patterns inside certain selected difficulty.
/// </summary>
[CustomEditor(typeof(PatternDatabase))]
public class PatternDataBaseEditor : Editor
{
    // variables 
    int selected;
    int DifficultyNumber;
    public PatternDatabase PatternDataBase;
    public Pattern AddedPattern;
    List<string> poolableList;

    //----------------------------------------------------------------------
    void OnEnable()
    {
        PatternDataBase = (PatternDatabase)target;
        DifficultyNumber = PatternDataBase.Count;
        selected = 0;
    }

    //-----------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UpdateDifficultyList();
        AddListOfPatternsToDifficulty();

        DifficultyNumber = EditorGUILayout.IntSlider("Number of difficulties:", DifficultyNumber, 1, 10);
        //-------------------------------------------------------
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Separator();
        //-----------------------------------------------------------
        selected = EditorGUILayout.Popup("Select Difficulty", selected, poolableList.ToArray());
        //-------------------------------------------------------
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Separator();
        //-----------------------------------------------------------

        DisplayDifficultyPatterns(selected);

        //------------------------------------------------------------
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Separator();
        //-----------------------------------------------------------
        // Add button and drag and drop object in it.

        AddedPattern = (Pattern)EditorGUILayout.ObjectField("Pattern:", AddedPattern, typeof(Pattern), false);

        if (GUILayout.Button("Add"))
        {
            AddPatternToDifficulty(selected, AddedPattern);
        }
    }

    void DisplayDifficultyPatterns(int selected)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Patterns in this level of Difficulty: ", EditorStyles.boldLabel);
        GUILayout.Label(selected.ToString());
        GUILayout.EndHorizontal();

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        for (int j = 0; j < PatternDataBase.PatternDBList[selected].Count; j++)
        {

            EditorGUILayout.BeginHorizontal("HelpBox");
            GUILayout.Label(j.ToString(), EditorStyles.centeredGreyMiniLabel);
            EditorGUILayout.LabelField(PatternDataBase.PatternDBList[selected][j].name, EditorStyles.miniLabel);

            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Delete"))
            {
                PatternDataBase.PatternDBList[selected].RemoveAt(j);
                return;
            }
        }
    }

    void UpdateDifficultyList()
    {
        poolableList = new List<string>();

        //--------------------------------------------------
        // Get the number of Difficulties 
        for (int i = 0; i < PatternDataBase.Count; i++)
        {
            // turn the number of Difficulties to string 
            string poolableStr = i.ToString();
            poolableList.Add(poolableStr);
        }
    }

    void AddListOfPatternsToDifficulty()
    {

        if (DifficultyNumber > PatternDataBase.Count)
        {

            int N = DifficultyNumber - PatternDataBase.Count;
            for (int i = 0; i < N; i++)
            {

                PatternDataBase.PatternDBList.Add(new Difficulty());

            }

        }
        else if (DifficultyNumber < PatternDataBase.Count)
        {
            int N = PatternDataBase.Count - DifficultyNumber;
            PatternDataBase.PatternDBList.RemoveRange(DifficultyNumber, N);
            if (selected > DifficultyNumber - 1)
            {
                selected = DifficultyNumber - 1;
            }
        }
    }

    void AddPatternToDifficulty(int selected, Pattern addedPattern)
    {

        if (!PatternDataBase.PatternDBList[selected].Contains(addedPattern))
        {
            PatternDataBase.PatternDBList[selected].Add(addedPattern);

        }
        else
        {
            EditorUtility.DisplayDialog("ADD Error", "This Pattern Already Exists", "OK");
        }

    }
}



