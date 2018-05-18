using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor to show and Delete the patterns inside certain selected difficulty.
/// </summary>
[CustomEditor(typeof(PatternDB))]
public class PatternDataBaseEditor : Editor
{
    // variables 
    int selected;
    int DifficultyNumber;
    string str;
    public PatternDB PatternDataBase;
    public PatternSO AddedPattern;
    List<string> options;

    //----------------------------------------------------------------------
    void OnEnable()
    {
        PatternDataBase = (PatternDB)target;
        DifficultyNumber = PatternDataBase.Count;
        selected = 0;
        AddinitialPatternsToTest();

    }

    //-----------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UpdateDifficultyList();
        AddListOfpatternstoDifficulty();



        options.Add(str);
        DifficultyNumber = EditorGUILayout.IntField("Difficulty Number:", DifficultyNumber);
        //-------------------------------------------------------
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Separator();
        //-----------------------------------------------------------
        selected = EditorGUILayout.Popup("Select The Difficulty", selected, options.ToArray());
        //-------------------------------------------------------
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Separator();
        //-----------------------------------------------------------

        DisplayPatterninthisLevel(selected);

        //------------------------------------------------------------
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Separator();
        //-----------------------------------------------------------
        // Add button and drag and drop object in it.

        AddedPattern = (PatternSO)EditorGUILayout.ObjectField("Added Pattern :", AddedPattern, typeof(PatternSO), false);

        if (GUILayout.Button("ADD"))
        {
            addpatterntoDifficulty(selected, AddedPattern);
        }




    }

    void DisplayPatterninthisLevel(int selected)
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

            // EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Delete"))
            {
                PatternDataBase.PatternDBList[selected].RemoveAt(j);

                return;
            }

            // EditorGUILayout.EndHorizontal();


        }
    }

    void UpdateDifficultyList()
    {
        options = new List<string>();

        //--------------------------------------------------
        // Get the number of Difficulties 
        for (int i = 0; i < PatternDataBase.Count; i++)
        {
            // turn the number of Difficulties to string 
            str = i.ToString();
            options.Add(str);


        }

    }
    void AddListOfpatternstoDifficulty()
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

    void addpatterntoDifficulty(int selected, PatternSO AddedPattern)
    {

        if (!PatternDataBase.PatternDBList[selected].Contains(AddedPattern))
        {
            PatternDataBase.PatternDBList[selected].Add(AddedPattern);

        }
        else
        {
            EditorUtility.DisplayDialog("ADD Erorr", "This Pattern is already Existed", "OK");
        }

    }

    void AddinitialPatternsToTest()
    {
        // Adding empty list of patterns to the database
        Difficulty PsoListD0 = new Difficulty();
        Difficulty PsoListD1 = new Difficulty();
        PsoListD0.Add(new PatternSO());
        PsoListD0.Add(new PatternSO());
        PsoListD0.Add(new PatternSO());

        PatternDataBase.PatternDBList.Add(PsoListD0);
        PatternDataBase.PatternDBList.Add(PsoListD1);
        DifficultyNumber = PatternDataBase.Count;
    }
}



