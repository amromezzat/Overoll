using System.Collections;
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
    int selectedDifficulty;
    int difficultyNum;
    public PatternDatabase patternDatabase;
    public Pattern patternToBeAdd;
    List<string> poolableList;

    //----------------------------------------------------------------------
    void OnEnable()
    {
        patternDatabase = (PatternDatabase)target;
        difficultyNum = patternDatabase.Count;
        selectedDifficulty = 0;
    }

    //-----------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        difficultyNum = EditorGUILayout.IntSlider("Number of Difficulties:", difficultyNum, 0, 10);
        UpdateDifficultyList();

        if (difficultyNum == 0)
        {
            return;
        }

        UpdateDropDownMenu();

        //-------------------------------------------------------
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Separator();
        //-----------------------------------------------------------
        selectedDifficulty = EditorGUILayout.Popup("Select Difficulty", selectedDifficulty, poolableList.ToArray());
        //-------------------------------------------------------
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Separator();
        //-----------------------------------------------------------

        DisplayCurrentDifficultyPatterns();

        //------------------------------------------------------------
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Separator();
        //-----------------------------------------------------------
        // Add button and drag and drop object in it.

        patternToBeAdd = (Pattern)EditorGUILayout.ObjectField("Pattern:", patternToBeAdd, typeof(Pattern), false);

        if (GUILayout.Button("Add"))
        {
            AddPatternToDifficulty(selectedDifficulty, patternToBeAdd);
        }
    }

    void DisplayCurrentDifficultyPatterns()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Patterns in this level of Difficulty: ", EditorStyles.boldLabel);
        GUILayout.Label(patternDatabase[selectedDifficulty].Count.ToString());
        GUILayout.EndHorizontal();

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        for (int i = 0; i < patternDatabase[selectedDifficulty].Count; i++)
        {

            EditorGUILayout.BeginHorizontal("HelpBox");
            GUILayout.Label(i.ToString(), EditorStyles.miniLabel);
            EditorGUILayout.LabelField(patternDatabase[selectedDifficulty][i].name, EditorStyles.helpBox);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Delete"))
            {
                patternDatabase[selectedDifficulty].RemoveAt(i);
                return;
            }
        }

        if (patternDatabase[selectedDifficulty].Count == 0)
        {
            EditorGUILayout.LabelField("Empty", EditorStyles.textField);
        }
    }

    void UpdateDropDownMenu()
    {
        poolableList = new List<string>();

        //--------------------------------------------------
        // Get the number of Difficulties 
        for (int i = 0; i < patternDatabase.Count; i++)
        {
            //add string expressing difficulty to list
            poolableList.Add(i.ToString());
        }
    }

    void UpdateDifficultyList()
    {

        if (difficultyNum > patternDatabase.Count)
        {

            int N = difficultyNum - patternDatabase.Count;
            for (int i = 0; i < N; i++)
            {
                patternDatabase.Add(new Difficulty());
            }

            if (difficultyNum == 1)
            {
                selectedDifficulty = 0;
            }

        }

        else if (difficultyNum < patternDatabase.Count)
        {
            int N = patternDatabase.Count - difficultyNum;
            patternDatabase.RemoveRange(difficultyNum, N);
            if (selectedDifficulty > difficultyNum - 1)
            {
                selectedDifficulty = difficultyNum - 1;
            }
        }
    }

    void AddPatternToDifficulty(int selected, Pattern patternToBeAdd)
    {

        if (!patternDatabase[selected].Contains(patternToBeAdd))
        {
            patternDatabase[selected].Add(patternToBeAdd);
        }
        else
        {
            EditorUtility.DisplayDialog("ADD Error", "This Pattern Already Exists", "OK");
        }

    }
}



