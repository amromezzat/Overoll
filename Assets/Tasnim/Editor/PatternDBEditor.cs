using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PatternDB))]
public class PatternDataBaseEditor : Editor
{


    //List<PatternSO> Difficulties;
    bool showDifficulty;

    public PatternDB PatternDataBase;
    public PatternSO patterso;
    //Variables to Edit 
    public PatternSO Difficulty;
    int DifficultyLevel;
    public PatternSO Lenght;
    //----------------------
    public Segment segment;

    void OnEnable()
    {
        PatternDataBase = (PatternDB)target;

        showDifficulty = true;
    }
   

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        for (int i = 0; i < PatternDataBase.Count; i++)
        {
            if(EditorGUILayout.Foldout(true, "Difficulty Level " + i+1))
            {

            }


        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Separator();



            
           
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();
        

       


    }
}

