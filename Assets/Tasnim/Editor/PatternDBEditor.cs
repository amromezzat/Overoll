//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;


//[CustomEditor(typeof(PatternDB))]
//public class PatternDataBaseEditor : Editor
//{


//    //List<PatternSO> Difficulties;
//    bool showDifficulty;

//    public PatternDB PatternDataBase;
//    int selected;

//    void OnEnable()
//    {
//        PatternDataBase = (PatternDB)target;

//        showDifficulty = true;

//        selected = 0;
//    }
   

//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        List<PatternSO> psoList = new List<PatternSO>();
//        PatternDataBase.PatternDBList.Add(psoList);
//     //   PatternDBList[2] = psoList;

//        List<string> options=new List<string>(5);

//        for (int i = 0; i < PatternDataBase.Count; i++)
//        {
//            string str = i.ToString();
//            options.Add(str);
//        }
//            selected = EditorGUILayout.Popup("Select The Difficulty", selected, options.ToArray());




//        //for (int i = 0; i < PatternDataBase.Count; i++)
//        //{
//        //    if(EditorGUILayout.Foldout(true, "Difficulty Level " + i+1))
//        //    {
//        //        DisplayPatterninthisLevel(i);

//        //        EditorGUILayout.Separator();
//        //        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
//        //        EditorGUILayout.Separator();

//        //    }


//        //}
//    }

//    void DisplayPatterninthisLevel(int i)
//    {
//        GUILayout.BeginHorizontal();
//        GUILayout.Label("Patterns in this level of Difficulty: ", EditorStyles.boldLabel);
//        GUILayout.Label(PatternDataBase.PatternDBList[i].Count.ToString());
//        GUILayout.EndHorizontal();

//        EditorGUILayout.Separator();
//        EditorGUILayout.Separator();

//        for (int j = 0; j < PatternDataBase.PatternDBList[i].Count; j++)
//        {
//            EditorGUILayout.BeginHorizontal("HelpBox");
//            GUILayout.Label(i.ToString(), EditorStyles.centeredGreyMiniLabel);
//            EditorGUILayout.LabelField(PatternDataBase.PatternDBList[i][j].name + "(" + PatternDataBase.PatternDBList[i].Count.ToString() + ")", EditorStyles.miniLabel);
//            EditorGUILayout.EndHorizontal();


//        }


//    }
//}

